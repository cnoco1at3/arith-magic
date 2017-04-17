using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using DG.Tweening;

public class ToolBoxBehavior : GenericSingleton<ToolBoxBehavior> {


    public AudioClip right_sfx;
    public AudioClip wrong_sfx;

    [SerializeField]
    private GameObject[] numbers_;

    [SerializeField]
    private GameObject[] operators_;

    [SerializeField]
    private PartsAcceptor[] slots_;

    [SerializeField]
    private ScrewContainer[] containers_;

    [SerializeField]
    private GameObject[] screws_;

    [SerializeField]
    private Transform[] anchors_;

    [SerializeField]
    private GameObject feedback_;

    [SerializeField]
    private GameObject wrong_feedback_;

    [SerializeField]
    private ToolBoxActiveButton button_;

    [SerializeField]
    private GameObject cover_;

    private GameObject[] problems_;
    private GameObject operator_;

    private int problem_size_;
    private int category_;
    [SerializeField]
    private const int kProblemSize = 3;


    public void PopulateProblem(int category, bool downward = false) {
        if (downward)
            category = UnityEngine.Random.Range(1, category);

        category_ = category;
        problem_size_ = kProblemSize;

        SetNewProblem(ProblemRuler.GetNewProblem(category_));

        // animations here
        transform.DOMove(Vector3.zero, 1.0f);
        InteractManager.Instance.LockInteractionForSeconds(1.0f);
    }

    public void CheckActivateStatus() {
        foreach (PartsAcceptor slot in slots_) {
            if (!slot.active)
                continue;
            if (!slot.IsOccupied()) {
                button_.ActiveButton(false);
                return;
            }
        }
        button_.ActiveButton(true);

    }

    public void CheckSolveStatus() {
        bool solved = true;

        foreach (PartsAcceptor slot in slots_) {
            if (!slot.active)
                continue;
            if (!slot.IsSolved()) {
                solved = false;
                slot.ClearSlot();
            }
        }

        // Animation here
        if (solved)
            StartCoroutine(SolvedCoroutine());
        else
            StartCoroutine(WrongCoroutine());
    }

    public ScrewContainer GetContainerById(int id) {
        try {
            if (id >= 0 && id < containers_.Length)
                return containers_[id];
        } catch (NullReferenceException) { }
        return null;
    }

    public ScrewContainer GetNextContainer(ScrewContainer container) {
        try {
            for (int i = 0; i < containers_.Length - 1; ++i)
                if (containers_[i].Equals(container))
                    return containers_[i + 1];
        } catch (NullReferenceException) { }
        return null;
    }

    public ScrewContainer GetPrevContainer(ScrewContainer container) {
        try {
            for (int i = 1; i < containers_.Length; ++i)
                if (containers_[i].Equals(container))
                    return containers_[i - 1];
        } catch (NullReferenceException) { }
        return null;
    }

    public GameObject GetScrewById(int id) {
        if (id >= 0 && id < screws_.Length)
            return screws_[id];
        return null;
    }

    public GameObject GetScrewByContainer(ScrewContainer container) {
        try {
            for (int i = 0; i < containers_.Length && i < screws_.Length; ++i)
                if (container.Equals(containers_[i]))
                    return screws_[i];
        } catch (NullReferenceException) { }
        return null;
    }

    private void SetNewProblem(ProblemData prob) {
        ClearProblem();
        SpawnOperator(GameController.add);
        SpawnProblem(prob.num1, prob.num2);

        int ans = GameController.add ? prob.num1 + prob.num2 : prob.num1 - prob.num2;
        RefineSlots(ans);
    }

    private void ClearProblem() {
        if (problems_ != null)
            foreach (GameObject problem in problems_)
                if (problem != null)
                    Destroy(problem);
        if (operators_ != null)
            Destroy(operator_);

        if (containers_ != null)
            foreach (ScrewContainer container in containers_)
                if (container != null)
                    container.ClearSlots();

        if (slots_ != null)
            foreach (PartsAcceptor slot in slots_)
                if (slot != null)
                    slot.ClearSlot();

        if (button_ != null)
            button_.ActiveButton(false);
    }

    private void SpawnOperator(bool add) {
        GameObject op = add ? operators_[0] : operators_[1];
        operator_ = Instantiate(op, anchors_[4].position, Quaternion.identity, transform);
    }

    private void SpawnProblem(int num1, int num2) {
        problems_ = new GameObject[4];

        problems_[0] = Instantiate(numbers_[num1 % 10], anchors_[0].position, Quaternion.identity, transform);
        problems_[0].GetComponent<ScrewGenerator>().GenerateScrews(0);
        if (num1 >= 10) {
            problems_[1] = Instantiate(numbers_[num1 / 10], anchors_[1].position, Quaternion.identity, transform);
            problems_[1].GetComponent<ScrewGenerator>().GenerateScrews(1);
        }

        problems_[2] = Instantiate(numbers_[num2 % 10], anchors_[2].position, Quaternion.identity, transform);
        problems_[2].GetComponent<ScrewGenerator>().GenerateScrews(0, GameController.add);
        if (num2 >= 10) {
            problems_[3] = Instantiate(numbers_[num2 / 10], anchors_[3].position, Quaternion.identity, transform);
            problems_[3].GetComponent<ScrewGenerator>().GenerateScrews(1, GameController.add);
        }
    }

    private void RefineSlots(int ans) {
        slots_[0].SetAccPartId(ans % 10);

        if (ans < 10) {
            slots_[1].active = false;
            slots_[1].GetComponent<Collider>().enabled = false;
            cover_.SetActive(true);
        } else {
            slots_[1].active = true;
            slots_[1].GetComponent<Collider>().enabled = true;
            slots_[1].SetAccPartId(ans / 10);
            cover_.SetActive(false);
        }
    }

    private IEnumerator SolvedCoroutine() {
        InteractManager.LockInteraction();
        feedback_.SetActive(true);
        SoundLib.SoundManager.Instance.PlaySFX(right_sfx);
        yield return new WaitForSeconds(2.0f);
        feedback_.SetActive(false);

        problem_size_--;
        if (problem_size_ > 0) {
            SetNewProblem(ProblemRuler.GetNewProblem(category_));
        } else {
            ClearProblem();
            transform.DOMove(new Vector3(0, 10.4f), 2.0f);
            XRayCameraBehavior.Instance.CheckParts(true);
        }
        InteractManager.ReleaseInteraction();
    }

    private IEnumerator WrongCoroutine() {
        InteractManager.LockInteraction();
        SoundLib.SoundManager.Instance.PlaySFX(wrong_sfx);
        wrong_feedback_.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        wrong_feedback_.SetActive(false);
        InteractManager.ReleaseInteraction();
    }
}
