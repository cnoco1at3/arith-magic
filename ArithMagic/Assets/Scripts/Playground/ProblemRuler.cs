using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public static class ProblemRuler {

    public static ProblemData GetNewProblem(int category) {

        Random.InitState((int)(Time.time * 19997) % 1000000);

        int num1, num2;

        switch (category) {
            case 1:
                num1 = Random.Range(1, 9);
                num2 = Random.Range(0, 9 - num1);
                break;
            case 2:
                num1 = Random.Range(10, 99);
                num2 = Random.Range(0, 9 - num1 % 10);
                break;
            case 3:
                num1 = Random.Range(10, 89);
                num2 = Random.Range(10, 99 - num1) / 10 * 10 + Random.Range(0, 9 - num1 % 10);
                break;
            case 4:
                num1 = Random.Range(1, 9);
                num2 = Random.Range(10 - num1, 9);
                break;
            case 5:
                num1 = Random.Range(10, 89);
                num2 = num1 % 10 == 0 ? 9 : Random.Range(10 - num1 % 10, 9);
                break;
            case 6:
                num1 = Random.Range(10, 79);
                num2 = Random.Range(10, 90 - num1) / 10 * 10;
                num2 += num1 % 10 == 0 ? 9 : Random.Range(10 - num1 % 10, 9);
                break;
            default:
                num1 = 0;
                num2 = 0;
                break;
        }

        return new ProblemData(num1, num2);
    }
}

public class ProblemData {
    public int num1;
    public int num2;
    public bool add;

    public ProblemData(int n1, int n2, bool af = true) {
        num1 = n1;
        num2 = n2;
        add = af;
    }
}
