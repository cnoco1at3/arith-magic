using UnityEngine;
using System.Collections.Generic;

namespace RecognizerLib {
    public class Resampler {

        public static List<Vector2> Resample(List<Vector2> points, int sample) {
            float l = PathLength(points) / (sample - 1),
                dd = 0.0f;
            List<Vector2> new_points = new List<Vector2>();

            for (int i = 1; i < points.Count; ++i) {
                float d = Vector2.Distance(points[i - 1], points[i]);
                if (dd + d >= l) {
                    float qx = points[i - 1].x + ((l - dd) / d) * (points[i].x - points[i - 1].x),
                        qy = points[i - 1].y + ((l - dd) / d) * (points[i].y - points[i - 1].y);
                    Vector2 q = new Vector2(qx, qy);
                    new_points.Add(q);
                    points.Insert(i, q);
                    dd = 0;
                }
                else
                    dd += d;
            }

            return new_points;
        }

        private static float PathLength(List<Vector2> points) {
            float d = 0;
            for (int i = 1; i < points.Count; ++i)
                d += Vector2.Distance(points[i - 1], points[i]);
            return d;
        }
    }

    public class Rotater {
        public static List<Vector2> RotateToZero(List<Vector2> points) {
            Vector2 c = new Vector2();
            foreach (Vector2 p in points)
                c += p / points.Count;
            float theta = Mathf.Atan2(c.y - points[0].y, c.x - points[0].x);
            return RotateBy(points, c, theta);
        }

        private static List<Vector2> RotateBy(List<Vector2> points, Vector2 centroid, float theta) {
            List<Vector2> new_points = new List<Vector2>();
            foreach (Vector2 p in points) {
                float qx = (p.x - centroid.x) * Mathf.Cos(theta) - (p.y - centroid.y) * Mathf.Sin(theta) + centroid.x,
                    qy = (p.x - centroid.x) * Mathf.Sin(theta) + (p.y - centroid.y) * Mathf.Cos(theta) + centroid.y;
                new_points.Add(new Vector2(qx, qy));
            }
            return new_points;
        }
    }

    public class Scaler {
        public static List<Vector2> ScaleToSquare(List<Vector2> points, float size) {
            float max_x = Mathf.NegativeInfinity,
                max_y = Mathf.NegativeInfinity,
                min_x = Mathf.Infinity,
                min_y = Mathf.Infinity;

            foreach (Vector2 p in points) {
                if (p.x > max_x)
                    max_x = p.x;
                if (p.x < min_x)
                    min_x = p.x;
                if (p.y > max_y)
                    max_y = p.y;
                if (p.y < min_y)
                    min_y = p.y;
            }

            for (int i = 0; i < points.Count; ++i) {
                float qx = points[i].x * (size / (max_x - min_x)),
                    qy = points[i].y * (size / (max_y - min_y));
                points[i] = new Vector2(qx, qy);
            }

            return points;
        }

        public static List<Vector2> TranslateToOrigin(List<Vector2> points) {
            Vector2 centroid = new Vector2();
            foreach (Vector2 p in points)
                centroid += p / points.Count;
            for(int i = 0; i < points.Count; ++i) {
                float qx = points[i].x - centroid.x,
                    qy = points[i].y - centroid.y;
                points[i] = new Vector2(qx, qy);
            }
            return points;
        }
    }
}
