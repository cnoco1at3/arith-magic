using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using DG.Tweening;

public class ToolBoxBehavior : GenericSingleton<ToolBoxBehavior> {


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

    private GameObject[] problems_;
    private GameObject operator_;

    void Start() { }

    void Update() {
    }

    public void PopulateProblem(int category, bool downward = false) {
        if (downward)
            category = Random.Range(1, category);

        SetNewProblem(ProblemRuler.GetNewProblem(category));

        // animations here
        transform.DOMove(Vector3.zero, 1.0f);
    }

    public void CheckSolveStatus() {
        foreach (PartsAcceptor slot in slots_)
            if (slot.active && !slot.IsSolved())
                return;

        // animations here
        StartCoroutine(SolvedCoroutine());
    }

    public ScrewContainer GetContainerById(int id) {
        if (id >= 0 && id < containers_.Length)
            return containers_[id];
        return null;
    }

    public GameObject GetScrewById(int id) {
        if (id >= 0 && id < screws_.Length)
            return screws_[id];
        return null;
    }

    private void SetNewProblem(ProblemData prob) {
        ClearProblem();
        SpawnOperator(prob.add);
        SpawnProblem(prob.num1, prob.num2);

        int ans = prob.add ? prob.num1 + prob.num2 : prob.num1 - prob.num2;
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
        problems_[2].GetComponent<ScrewGenerator>().GenerateScrews(0);
        if (num2 >= 10) {
            problems_[3] = Instantiate(numbers_[num2 / 10], anchors_[3].position, Quaternion.identity, transform);
            problems_[3].GetComponent<ScrewGenerator>().GenerateScrews(1);
        }
    }

    private void RefineSlots(int ans) {
        slots_[0].SetAccPartId(ans % 10);

        if (ans < 10) {
            slots_[1].active = false;
            slots_[1].GetComponent<Collider>().enabled = false;
        } else {
            slots_[1].active = true;
            slots_[1].GetComponent<Collider>().enabled = true;
            slots_[1].SetAccPartId(ans / 10);
        }
    }

    private IEnumerator SolvedCoroutine() {
        yield return new WaitForSeconds(2.0f);
        transform.DOMove(new Vector3(0, 10.4f), 2.0f);

        XRayCameraBehavior.Instance.CheckParts(true);
    }
}
