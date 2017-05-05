using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;
using AvatarLib;

public static class ProblemRuler {

    private static int[] k_category_ = new int[18];
    private static int[] f_category_ = new int[18] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2 };
    private static int[] s_category_ = new int[18] { 0, 1, 2, 3, 3, 3, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5 };

    private static ProblemData prev;

    public static ProblemData GetNewProblem(int category) {
        Random.InitState((int)(Time.time * 19891) % 19991);

        ProblemData tmp = GetProblem(category);
        if (prev != null) {
            int cnt = 0, max = 3;
            while (tmp.Equals(prev) && cnt < max) {
                tmp = GetProblem(category);
                cnt++;
            }
        }
        prev = tmp;

        return new ProblemData(tmp);
    }

    public static ProblemData GetProblem(int category) {
        int num1, num2;

        switch (category) {
            // additions
            case 1:
                num1 = Random.Range(1, 10);
                num2 = RandomSampler.Sample10(0, 10 - num1);
                break;
            case 2:
                num1 = Random.Range(10, 100);
                num2 = RandomSampler.Sample10(0, 10 - num1 % 10);
                break;
            case 3:
                num1 = Random.Range(10, 90);
                num2 = Random.Range(10, 100 - num1) / 10 * 10 + RandomSampler.Sample10(0, 10 - num1 % 10);
                break;
            case 4:
                num1 = Random.Range(1, 10);
                num2 = Random.Range(10 - num1, 10);
                break;
            case 5:
                num1 = Random.Range(10, 90);
                num2 = num1 % 10 == 0 ? 9 : Random.Range(10 - num1 % 10, 10);
                break;
            case 6:
                num1 = Random.Range(10, 80);
                num2 = Random.Range(10, 90 - num1) / 10 * 10;
                num2 += num1 % 10 == 0 ? 9 : Random.Range(10 - num1 % 10, 10);
                break;

            // subtractions
            // s - s w/o re
            case 7:
                num1 = Random.Range(1, 10);
                num2 = RandomSampler.Sample10(0, num1 + 1);
                break;
            // d - s w/o re
            case 8:
                num1 = Random.Range(10, 19);
                num2 = RandomSampler.Sample10(0, num1 % 10 + 1);
                break;
            // d - d w/o re
            case 9:
                num1 = Random.Range(10, 100);
                num2 = Random.Range(1, num1 / 10 + 1) * 10 + RandomSampler.Sample10(0, num1 % 10 + 1);
                break;
            // d - s w/ re
            case 10:
                num1 = Random.Range(10, 19);
                num2 = RandomSampler.Sample10(num1 % 10 + 1, 10);
                break;
            // d - s w/ re
            case 11:
                num1 = Random.Range(20, 100);
                if (num1 % 10 == 9)
                    num1--;
                num2 = RandomSampler.Sample10(num1 % 10 + 1, 10);
                break;
            // d - d w/ re
            case 12:
                num1 = Random.Range(30, 100);
                if (num1 % 10 == 9)
                    num1--;
                num2 = Random.Range(1, num1 / 10 - 1) * 10 + Random.Range(num1 % 10 + 1, 10);
                break;

            default:
                num1 = 0;
                num2 = 0;
                break;
        }

        return new ProblemData(num1, num2);
    }

    public static int GetCategory(int level, int grade) {
        switch (grade) {
            case 0:
                return k_category_[level];
            case 1:
                return f_category_[level];
            case 2:
                return s_category_[level];
            default:
                return 0;
        }
    }
}

public class ProblemData {
    public int num1;
    public int num2;

    public ProblemData() {
        num1 = 0;
        num2 = 0;
    }

    public ProblemData(int n1, int n2) {
        num1 = n1;
        num2 = n2;
    }

    public ProblemData(ProblemData other) {
        if (other != null) {
            num1 = other.num1;
            num2 = other.num2;
        } else {
            num1 = 0;
            num2 = 0;
        }
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    public override bool Equals(object obj) {
        return num1 == ((ProblemData)obj).num1 && num2 == ((ProblemData)obj).num2;
    }
}
