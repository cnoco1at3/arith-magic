using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RandomSampler {

    private static float[] dist_ = { 0.055f, 0.105f, 0.105f, 0.105f, 0.105f, 0.105f, 0.105f, 0.105f, 0.105f, 0.105f };

    public static int Sample10(int min = 0, int max = 10) {
        if (min < 0 || max > 10)
            throw new IndexOutOfRangeException();

        float l_b = 0.0f;
        for (int i = 0; i < min; ++i)
            l_b += dist_[i];

        float u_b = l_b;
        for (int i = min; i < max; ++i)
            u_b += dist_[i];

        float sample = UnityEngine.Random.Range(l_b, u_b);
        float l_thres = 0.0f;
        float u_thres = dist_[0];
        for (int i = 0; i < dist_.Length; ++i) {
            if (l_thres <= sample && u_thres >= sample)
                return i;
            l_thres += dist_[i];
            u_thres += dist_[i + 1];
        }
        return 9;
    }
}
