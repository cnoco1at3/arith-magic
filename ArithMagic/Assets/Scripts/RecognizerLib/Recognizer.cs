using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RecognizerLib {
    public class Recognizer {

        private Dictionary<string, List<Vector2>> templates;

        public Recognizer(Dictionary<string, List<Vector2>> templates) {
            this.templates = templates;
        }

        public string Recognize() {
            float b = Mathf.Infinity;
            string match;
            foreach(KeyValuePair<string, List<Vector2>> template in templates) {
                match = template.Key;
            }

            return "";
        }

        private float DistanceAtBestAngle(List<Vector2> points, List<Vector2> template, float theta_a, float theta_b, float theta_delta) {
            return 0;
        }

        private float DistanceAtAngle(List<Vector2> points, List<Vector2> template, float theta) {
            return 0;
        }

        private float PathDistance(List<Vector2> a, List<Vector2> b) {
            float d = 0;
            for (int i = 0; i < a.Count; ++i)
                d += Vector2.Distance(a[i], b[i]);
            return d;
        }
    }
}
