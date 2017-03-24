using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class ToolBoxBehavior : GenericSingleton<ToolBoxBehavior> {

    [SerializeField]
    private GameObject[] ones_;

    [SerializeField]
    private GameObject[] tens_;

    [SerializeField]
    private GameObject[] operators_;

    [SerializeField]
    private PartsAcceptor[] slots_;

    [SerializeField]
    private ScrewContainer[] containers_;

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
    }

    private void SpawnOperator(bool add) {
        GameObject op = add ? operators_[0] : operators_[1];
        operator_ = Instantiate(op, anchors_[4].position, Quaternion.identity, transform);
    }

    private void SpawnProblem(int num1, int num2) {
        problems_ = new GameObject[4];

        problems_[0] = Instantiate(ones_[num1 % 10], anchors_[0].position, Quaternion.identity, transform);
        if (num1 >= 10)
            problems_[1] = Instantiate(tens_[num1 / 10], anchors_[1].position, Quaternion.identity, transform);

        problems_[2] = Instantiate(ones_[num2 % 10], anchors_[2].position, Quaternion.identity, transform);
        if (num2 >= 10)
            problems_[3] = Instantiate(tens_[num2 / 10], anchors_[3].position, Quaternion.identity, transform);
    }

    private void RefineSlots(int ans) {
        if (ans < 10) 
            slots_[1].GetComponent<Collider>().enabled = false;
        else
            slots_[1].GetComponent<Collider>().enabled = true;
    }
}
