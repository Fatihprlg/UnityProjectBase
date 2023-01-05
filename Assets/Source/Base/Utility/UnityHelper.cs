namespace Helpers
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    public static class Vectors
    {
        public static Vector3 RotatePointAround(Vector3 point, Vector3 offset, Quaternion angle)
        {
            return angle * (point - offset) + offset;
        }

        public static Vector3 MoveWithPercent(Vector3 start, Vector3 target, float percent)
        {
            Vector3 dir = (target - start).normalized;
            float distance = Vector3.Distance(start, target);
            Vector3 pos = start + (dir * distance * percent);
            return pos;
        }

        public static Vector2 WorldToScreenPoint(Camera camera, Canvas canvas, Vector3 targetPos)
        {
            Vector2 myPositionOnScreen = camera.WorldToScreenPoint(targetPos);
            float scaleFactor = canvas.scaleFactor;
            return new Vector2(myPositionOnScreen.x / scaleFactor, myPositionOnScreen.y / scaleFactor);
        }

        public static Vector2 ToVector2(float angle)
        {

            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        }

        public static Vector2 AngleToVector2(float angle)
        {
            return new Vector2((float)Mathf.Sin(angle), -(float)Mathf.Cos(angle));
        }

        public static float ToAngle(Vector2 value)
        {
            return Mathf.Atan2(value.x, value.y) * Mathf.Rad2Deg;
        }

        public static List<Vector2> OrderbyAngle(List<Vector2> values)
        {
            return values.OrderBy(x => Mathf.Atan2(x.x, x.y)).ToList();
        }


        public static Vector3 ToYZ(Vector2 value)
        {
            return new Vector3(0, value.y, value.x);
        }

        public static Vector3 ToXZ(Vector2 value)
        {
            return new Vector3(value.x, 0, value.y);
        }

        public static Vector2 ClampVector2(Vector2 target, float xMax, float yMax)
        {
            float x = target.x;
            float y = target.y;

            x = Mathf.Clamp(x, -xMax, xMax);
            y = Mathf.Clamp(y, -yMax, yMax);

            return new Vector2(x, y);
        }

        public static Vector2 ClampVector2(Vector2 target, float xMin, float xMax, float yMin, float yMax)
        {
            float x = target.x;
            float y = target.y;

            x = Mathf.Clamp(x, xMin, xMax);
            y = Mathf.Clamp(y, yMin, yMax);

            return new Vector2(x, y);
        }

        public static Vector2 ClampVector2(Vector2 target, Vector2 minValues, Vector2 maxValues)
        {
            float x = target.x;
            float y = target.y;

            x = Mathf.Clamp(x, minValues.x, maxValues.x);
            y = Mathf.Clamp(y, minValues.y, maxValues.y);

            return new Vector2(x, y);
        }

        public static Vector2 RandomVector2(float maxValues)
        {
            float x = Random.Range(-maxValues, maxValues);
            float y = Random.Range(-maxValues, maxValues);
            return new Vector2(x, y);
        }

        public static Vector2 RandomVector2(float minValue, float maxValue)
        {
            float random = Random.Range(minValue, maxValue);
            return new Vector2(random, random);
        }

        public static Vector2 RandomVector2(float xMin, float xMax, float yMin, float yMax)
        {
            float x = Random.Range(xMin, xMax);
            float y = Random.Range(yMin, yMax);

            return new Vector2(x, y);
        }

        public static Vector2 RandomVector2(Vector2 minVlaues, Vector2 maxValues)
        {
            float x = Random.Range(minVlaues.x, maxValues.x);
            float y = Random.Range(minVlaues.y, maxValues.y);
            return new Vector2(x, y);
        }

        public static Vector3 ClampVector3(Vector3 target, float xMax, float yMax, float zMax)
        {
            float x = target.x;
            float y = target.y;
            float z = target.z;

            x = Mathf.Clamp(x, -xMax, xMax);
            y = Mathf.Clamp(y, -yMax, yMax);
            z = Mathf.Clamp(z, -zMax, zMax);

            return new Vector3(x, y, z);
        }

        public static Vector3 ClampVector3(Vector3 target, float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
        {
            float x = target.x;
            float y = target.y;
            float z = target.z;

            x = Mathf.Clamp(x, xMin, xMax);
            y = Mathf.Clamp(y, yMin, yMax);
            z = Mathf.Clamp(z, zMin, zMax);

            return new Vector3(x, y, z);
        }

        public static Vector3 ClampVector3(Vector3 target, Vector3 minValues, Vector3 maxValues)
        {
            float x = target.x;
            float y = target.y;
            float z = target.z;

            x = Mathf.Clamp(x, minValues.x, maxValues.x);
            y = Mathf.Clamp(y, minValues.y, maxValues.y);
            z = Mathf.Clamp(z, minValues.z, maxValues.z);

            return new Vector3(x, y, z);
        }

        public static Vector3 RandomVector3(float maxValues)
        {
            float x = Random.Range(-maxValues, maxValues);
            float y = Random.Range(-maxValues, maxValues);
            float z = Random.Range(-maxValues, maxValues);

            return new Vector3(x, y, z);
        }

        public static Vector3 RandomVector3(float minValue, float maxValue)
        {
            float random = Random.Range(minValue, maxValue);
            return new Vector3(random, random, random);
        }

        public static Vector3 RandomVector3(float xMin, float xMax, float yMin, float yMax, float zmin, float zMax)
        {
            float x = Random.Range(xMin, xMax);
            float y = Random.Range(yMin, yMax);
            float z = Random.Range(zmin, zMax);

            return new Vector3(x, y, z);
        }

        public static Vector3 RandomVector3(Vector3 minVlaues, Vector3 maxValues)
        {
            float x = Random.Range(minVlaues.x, maxValues.x);
            float y = Random.Range(minVlaues.y, maxValues.y);
            float z = Random.Range(minVlaues.z, maxValues.z);
            return new Vector3(x, y, z);
        }

        public static bool GetIntersectionPointInLine(Vector2 aStartPoint, Vector2 aEndPoint, Vector2 bStartPoint, Vector2 bEndPoint, bool checkALine, out Vector2 intersectionPos)
        {
            bool isIntersects = GetIntersectionPoint(aStartPoint, aEndPoint, bStartPoint, bEndPoint, out intersectionPos);
            if (isIntersects)
            {
                if (checkALine)
                {
                    return IsPointBetweenTwoVector(aStartPoint, aEndPoint, intersectionPos);
                }
                else
                {
                    return IsPointBetweenTwoVector(bStartPoint, bEndPoint, intersectionPos);
                }
            }

            return false;
        }

        public static bool GetIntersectionPoint(Vector2 aStartPoint, Vector2 aEndPoint, Vector2 bStartPoint, Vector2 bEndPoint, out Vector2 intersectPos)
        {
            float tmp = (bEndPoint.x - bStartPoint.x) * (aEndPoint.y - aStartPoint.y) - (bEndPoint.y - bStartPoint.y) * (aEndPoint.x - aStartPoint.x);

            if (tmp == 0)
            {
                intersectPos = Vector2.zero;
                return false;
            }

            float mu = ((aStartPoint.x - bStartPoint.x) * (aEndPoint.y - aStartPoint.y) - (aStartPoint.y - bStartPoint.y) * (aEndPoint.x - aStartPoint.x)) / tmp;
            intersectPos = new Vector2(bStartPoint.x + (bEndPoint.x - bStartPoint.x) * mu, bStartPoint.y + (bEndPoint.y - bStartPoint.y) * mu);

            return true;
        }

        public static bool IsPointBetweenTwoVector(Vector3 aStartPoint, Vector3 aEndPoint, Vector3 point)
        {
            return Vector3.Dot((aEndPoint - aStartPoint).normalized, (point - aEndPoint).normalized) <= 0f && Vector3.Dot((aStartPoint - aEndPoint).normalized, (point - aStartPoint).normalized) <= 0f;
        }

        public static bool IsValuesInSpace(float aPoint, float bPoint, float min, float max)
        {
            if (aPoint >= min && aPoint <= max)
            {
                return true;
            }
            else if (bPoint >= min && bPoint <= max)
            {
                return true;
            }


            if (aPoint > bPoint)
            {
                if (aPoint > max && bPoint < min)
                {
                    return true;
                }
            }
            else if (bPoint > aPoint)
            {
                if (bPoint > max && aPoint < min)
                {
                    return true;
                }
            }

            return false;
        }

        public static Vector3 RoundV3(Vector3 value, int digits)
        {
            return new Vector3((float)System.Math.Round(value.x, digits), (float)System.Math.Round(value.y, digits), (float)System.Math.Round(value.z, digits));
        }

    }

    public static class String
    {
        public static string SortArray(object[] values, char seperationChar)
        {
            string newText = "";
            var lastObject = values.LastOrDefault();
            foreach (var item in values)
            {
                if (item.Equals(lastObject))
                {
                    newText += item.ToString();
                }
                else
                {
                    newText += item.ToString() + seperationChar;
                }
            }
            return newText;
        }

        public static string SortByMold(object[] values, char seperationChar)
        {
            string newText = "";
            var lastObject = values.LastOrDefault();
            foreach (var item in values)
            {
                if (item.Equals(lastObject))
                {
                    newText += "[" + item.ToString() + "]";
                }
                else
                {
                    newText += "[" + item.ToString() + "]" + seperationChar;
                }
            }
            return newText;
        }

        public static string SortArrayVariable(object[] values, char seperationChar)
        {
            string newText = "";
            var lastObject = values.LastOrDefault();
            foreach (var item in values)
            {
                if (item.Equals(lastObject))
                {
                    newText += "'" + item.ToString() + "'";
                }
                else
                {
                    newText += "'" + item.ToString() + "'" + seperationChar;
                }
            }
            return newText;
        }

        public static string SyncDictionary(Dictionary<string, object> values, char seperationChar)
        {
            string newText = "";
            var lastObject = values.LastOrDefault();
            foreach (var item in values)
            {
                if (item.Equals(lastObject))
                {
                    newText += item.Key + "='" + item.Value + "'";
                }
                else
                {
                    newText += item.Key + "='" + item.Value + "'" + seperationChar;
                }
            }
            return newText;
        }

        public static string SyncArray(string[] objects, object[] values, char seperationChar)
        {
            string newText = "";
            string lastObject = objects.LastOrDefault();

            for (int i = 0; i < objects.Length; i++)
            {
                if (objects[i] == lastObject)
                {
                    if (values[i] != null)
                        newText += objects[i] + "='" + values[i] + "'";
                }
                else
                {
                    if (values[i] != null)
                        newText += objects[i] + "='" + values[i] + "'" + seperationChar;
                }
            }
            return newText;
        }
    }

    public static class Maths
    {
        public static float GetValueWithPercent(float aValue, float bValue, float percent)
        {
            float diff = bValue - aValue;
            return aValue + (diff * percent);
        }


        public static float RoundDecimals(float target)
        {
            return (Mathf.Round(target * 100.0f) * 0.01f);
        }

        public static Vector2 RoundDecimals(Vector2 target)
        {
            return new Vector2(Mathf.Round(target.x * 100.0f) * 0.01f, Mathf.Round(target.y * 100.0f) * 0.01f);
        }

        public static bool IsValueInRange(float value, float min, float max)
        {
            if (value >= min && value <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector3 CalculateCubicBezierPoint(float time, Vector3 startPoint, Vector3 controlPoint1, Vector3 controlPoint2, Vector3 endPoint)
        {
            float u = 1 - time;
            float tt = time * time;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * time;

            Vector3 p = uuu * startPoint;
            p += 3 * uu * time * controlPoint1;
            p += 3 * u * tt * controlPoint2;
            p += ttt * endPoint;

            return p;
        }

        public static Vector2 CalculateCubicBezierPoint(float time, Vector2 startPoint, Vector2 controlPoint1, Vector2 controlPoint2, Vector2 endPoint)
        {
            float u = 1 - time;
            float tt = time * time;
            float uu = u * u;
            float uuu = uu * u;
            float ttt = tt * time;

            Vector2 p = uuu * startPoint;
            p += 3 * uu * time * controlPoint1;
            p += 3 * u * tt * controlPoint2;
            p += ttt * endPoint;

            return p;
        }

        public static Vector3 CalculateBezierPoint(float time, Vector3 startPoint, Vector3 controlPoint1, Vector3 endPoint)
        {
            time = Mathf.Clamp01(time);
            float oneMinusT = 1f - time;
            return oneMinusT * oneMinusT * startPoint + 2f * oneMinusT * time * controlPoint1 + time * time * endPoint;
        }

        public static double Round(double value, int digits)
        {
            return System.Math.Round(value, digits);
        }

        public static bool IsAngleInRange(float angle, float targetAngle, float decreaseVal, float increaseVal)
        {
            if (angle > targetAngle)
            {
                if (angle <= 360)
                {
                    if (targetAngle + increaseVal > 360)
                    {
                        return true;
                    }
                }

                if (angle <= targetAngle + increaseVal)
                {
                    return true;
                }

                if (targetAngle - decreaseVal < 0)
                {
                    float newMax = SubtractWithMax(targetAngle, decreaseVal, 360, 0);

                    if (angle >= newMax)
                    {
                        return true;
                    }
                }

            }
            else if (targetAngle == angle)
            {
                return true;
            }
            else
            {
                if (targetAngle - decreaseVal < 0)
                {
                    return true;
                }

                if (angle >= targetAngle - decreaseVal)
                {
                    return true;
                }

                if (targetAngle + decreaseVal > 360)
                {
                    float newMin = AdditionWithMax(targetAngle, increaseVal, 360, 0);

                    if (angle <= newMin)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static float ToDegree(float x, float y)
        {
            float value = (float)((Mathf.Atan2(x, y) / Mathf.PI) * 180f);
            if (value < 0) value += 360f;

            return value;
        }

        public static float AdditionWithMax(float value, float addValue, float maxValue, float min)
        {
            if (addValue > maxValue)
            {
                int diff = Mathf.FloorToInt(addValue / maxValue);
                addValue -= diff * maxValue;
            }


            if (value + addValue > maxValue)
            {
                return min + (value + addValue) - maxValue;
            }
            else
            {
                return value + addValue;
            }
        }

        public static float SubtractWithMax(float value, float subtractValue, float maxValue, float min)
        {
            if (subtractValue > maxValue)
            {
                int diff = Mathf.FloorToInt(subtractValue / maxValue);
                subtractValue -= diff * maxValue;
            }

            if (value - subtractValue < min)
            {
                return maxValue - (value + subtractValue);
            }
            else
            {
                return value - subtractValue;
            }
        }

        public static int SubtractWithMax(int value, int subtractValue, int maxValue, int min)
        {
            if (subtractValue > maxValue)
            {
                int diff = Mathf.FloorToInt(subtractValue / maxValue);
                subtractValue -= diff * maxValue;
            }

            if (value - subtractValue < min)
            {
                return maxValue - (value + subtractValue);
            }
            else
            {
                return value - subtractValue;
            }
        }


        public static float GetLinearValue(float min, float max, int index, int maxCount, bool clampValue = true)
        {
            float value = (max - min) / maxCount;
            value = min + (value * index);

            if (clampValue)
            {
                value = value > max ? max : value;
                value = value < min ? min : value;
            }

            return value;
        }

        public static float GetLinearValue(float min, float max, float currentValue, float maxValue, bool clampValue = true)
        {
            float value = (max - min) / maxValue;
            value = min + (value * currentValue);

            if (clampValue)
            {
                value = value > max ? max : value;
                value = value < min ? min : value;
            }
            return value;
        }

        public static Vector3 GetLinearValue(Vector3 start, Vector3 end, int index, int maxCount, bool clampValue = true)
        {
            return new Vector3(GetLinearValue(start.x, end.x, index, maxCount, clampValue), GetLinearValue(start.y, end.y, index, maxCount, clampValue), GetLinearValue(start.z, end.z, index, maxCount, clampValue));
        }


        public static float Lerp(float a, float b, float t)
        {
            return (1 - t) * a + t * b;
        }

        public static float GetValueByLuck(float[] lucks)
        {
            float total = 0;

            foreach (var item in lucks)
            {
                total += item;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < lucks.Length; i++)
            {
                if (randomPoint < lucks[i])
                {
                    return lucks[i];
                }
                else
                {
                    randomPoint -= lucks[i];
                }
            }

            return lucks.Length - 1;
        }

        public static float GetValueByLuck(List<float> lucks)
        {
            float total = 0;

            foreach (var item in lucks)
            {
                total += item;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < lucks.Count; i++)
            {
                if (randomPoint < lucks[i])
                {
                    return lucks[i];
                }
                else
                {
                    randomPoint -= lucks[i];
                }
            }

            return lucks.Count - 1;
        }

        public static int GetItemByLuck(List<float> lucks)
        {
            float total = 0;

            foreach (var item in lucks)
            {
                total += item;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < lucks.Count; i++)
            {
                if (randomPoint < lucks[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= lucks[i];
                }
            }

            return lucks.Count - 1;
        }

        public static int GetItemByLuck(float[] lucks)
        {
            float total = 0;

            foreach (var item in lucks)
            {
                total += item;
            }

            float randomPoint = Random.value * total;

            for (int i = 0; i < lucks.Length; i++)
            {
                if (randomPoint < lucks[i])
                {
                    return i;
                }
                else
                {
                    randomPoint -= lucks[i];
                }
            }

            return lucks.Length - 1;
        }

    }

    public static class Collections
    {
        public static T[] GetArray<T>(params T[] values)
        {
            return values;
        }

        public static List<T> ChangeIndex<T>(List<T> list, int changeIndex, int toIndex)
        {
            if (changeIndex == toIndex)
                return list;

            object a = list[changeIndex];
            object b = list[toIndex];

            list.Remove((T)a);
            list.Remove((T)b);

            if (changeIndex > toIndex)
            {
                list.Insert(toIndex, (T)a);
                list.Insert(changeIndex, (T)b);
            }
            else
            {
                list.Insert(changeIndex, (T)b);
                list.Insert(toIndex, (T)a);
            }

            return list;
        }

        public static List<T> Shufflelist<T>(List<T> targetList)
        {
            System.Random r = new System.Random();
            for (int i = 0; i < targetList.Count; i++)
            {
                var obj = targetList[i];
                targetList.Remove(obj);
                int index = r.Next(0, targetList.Count);
                targetList.Insert(index, obj);
            }

            return targetList;
        }

        public static List<T> ReverseList<T>(List<T> list)
        {
            int count = list.Count;

            for (int i = 0; i < count; i++)
            {
                var last = list[count - 1];
                list.Remove(last);
                list.Insert(i, last);
            }

            return list;
        }

        public static List<float> Round(List<float> values, int digits)
        {
            for (int i = 0; i < values.Count; i++)
            {
                values[i] = (float)System.Math.Round(values[i], digits);
            }

            return values;
        }

    }

    public static class Colors
    {
        public static Color GetColorWithPercent(Color a, Color b, float percent)
        {
            return new Color(Maths.GetValueWithPercent(a.r, b.r, percent), Maths.GetValueWithPercent(a.g, b.g, percent), Maths.GetValueWithPercent(a.b, b.b, percent));
        }

        public static bool IsEqual(Color a, Color b)
        {
            return a.r == b.r && a.g == b.g && a.b == b.b;
        }

        public static Color InvertColor(Color color)
        {
            return new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b);
        }

        public static Color ToColor(string str)
        {
            if (str.Contains("#") == false)
            {
                str = "#" + str;
            }

            Color color;
            ColorUtility.TryParseHtmlString(str, out color);
            return color;
        }

        public static string ToString(Color color)
        {
            return ColorUtility.ToHtmlStringRGBA(color);
        }

        public static Color SetAlpha(Color targetColor, float alphaValue)
        {
            Color alphaColor = targetColor;
            alphaColor.a = alphaValue;

            return alphaColor;
        }

        public static bool IsColorInRange(float sensitve, Color color, Color targetColor)
        {
            return Maths.IsValueInRange(color.r, targetColor.r - sensitve, targetColor.r + sensitve) && Maths.IsValueInRange(color.g, targetColor.g - sensitve, targetColor.g + sensitve) && Maths.IsValueInRange(color.b, targetColor.b - sensitve, targetColor.b + sensitve);
        }

    }

    public static class Meshs
    {
        public static List<Vector3> GetOutlineVertices(Mesh mesh, float height)
        {
            List<Vector3> points = new List<Vector3>();
            int[] triangles = mesh.triangles;
            Vector3[] vertices = mesh.vertices;
            Dictionary<string, KeyValuePair<int, int>> edges = new Dictionary<string, KeyValuePair<int, int>>();
            for (int i = 0; i < triangles.Length; i += 3)
            {
                for (int e = 0; e < 3; e++)
                {
                    int vert1 = triangles[i + e];
                    int vert2 = triangles[i + e + 1 > i + 2 ? i : i + e + 1];
                    string edge = Mathf.Min(vert1, vert2) + ":" + Mathf.Max(vert1, vert2);
                    if (edges.ContainsKey(edge))
                    {
                        edges.Remove(edge);
                    }
                    else
                    {
                        edges.Add(edge, new KeyValuePair<int, int>(vert1, vert2));
                    }
                }
            }

            Dictionary<int, int> lookup = new Dictionary<int, int>();
            foreach (KeyValuePair<int, int> edge in edges.Values)
            {
                if (lookup.ContainsKey(edge.Key) == false)
                {
                    lookup.Add(edge.Key, edge.Value);
                }
            }

            Vector3 bringFoward = new Vector3(0f, 0f, -0.1f);

            // Loop through edge vertices in order
            int startVert = 0;
            int nextVert = startVert;
            int highestVert = startVert;

            while (true)
            {
                points.Add(vertices[nextVert] + bringFoward);

                // Get next vertex
                nextVert = lookup[nextVert];

                // Store highest vertex (to know what shape to move to next)
                if (nextVert > highestVert)
                {
                    highestVert = nextVert;
                }

                // Shape complete
                if (nextVert == startVert)
                {

                    // Finish this shape's line
                    points.Add(vertices[nextVert] + bringFoward);

                    // Go to next shape if one exists
                    if (lookup.ContainsKey(highestVert + 1))
                    {

                        // Set starting and next vertices
                        startVert = highestVert + 1;
                        nextVert = startVert;

                        // Continue to next loop
                        continue;
                    }

                    // No more verts
                    break;
                }
            }

            points = points.Distinct().ToList();

            points.RemoveAll(x => x.y != height);

            return points;
        }

        public static bool IsPointInPolygon(Vector2 point, Vector2[] polygon)
        {
            int polygonLength = polygon.Length, i = 0;
            bool inside = false;
            // x, y for tested point.
            float pointX = point.x, pointY = point.y;
            // start / end point for the current polygon segment.
            float startX, startY, endX, endY;
            Vector2 endPoint = polygon[polygonLength - 1];
            endX = endPoint.x;
            endY = endPoint.y;
            while (i < polygonLength)
            {
                startX = endX; startY = endY;
                endPoint = polygon[i++];
                endX = endPoint.x; endY = endPoint.y;
                //
                inside ^= (endY > pointY ^ startY > pointY) /* ? pointY inside [startY;endY] segment ? */
                          && /* if so, test if it is under the segment */
                          ((pointX - endX) < (pointY - endY) * (startX - endX) / (startY - endY));
            }
            return inside;
        }

        public static Vector3[] GetABEdge(Mesh mesh, int triangleIndex)
        {
            if (triangleIndex < 0)
                return null;

            if (triangleIndex < (mesh.triangles.Length / 3))
            {
                return Collections.GetArray(mesh.vertices[mesh.triangles[triangleIndex * 3]], mesh.vertices[mesh.triangles[(triangleIndex * 3) + 1]], mesh.vertices[mesh.triangles[(triangleIndex * 3) + 2]]);
            }
            else
            {
                return null;
            }
        }

        public static Vector3[] GetBCEdge(Mesh mesh, int triangleIndex)
        {
            if (triangleIndex < 0)
                return null;

            if (triangleIndex < (mesh.triangles.Length / 3))
            {
                return Collections.GetArray(mesh.vertices[mesh.triangles[(triangleIndex * 3) + 1]], mesh.vertices[mesh.triangles[(triangleIndex * 3) + 2]], mesh.vertices[mesh.triangles[(triangleIndex * 3) + 2]]);
            }
            else
            {
                return null;
            }
        }

        public static Vector3[] GetCAEdge(Mesh mesh, int triangleIndex)
        {
            if (triangleIndex < 0)
                return null;

            if (triangleIndex < (mesh.triangles.Length / 3))
            {
                return Collections.GetArray(mesh.vertices[mesh.triangles[(triangleIndex * 3)]], mesh.vertices[mesh.triangles[(triangleIndex * 3) + 2]], mesh.vertices[mesh.triangles[(triangleIndex * 3) + 2]]);
            }
            else
            {
                return null;
            }
        }

        public static Vector3 GetIntersectionPoint(Vector3 aPoint, Vector3 bPoint, float searchValue, Vector3 searchPattern)
        {
            float x = 0;
            float y = 0;
            float z = 0;
            Vector2 intersectionPoint = Vector2.zero;

            if (searchPattern == Vector3.up)
            {
                if (Vectors.IsValuesInSpace(aPoint.y, bPoint.y, searchValue, searchValue))
                {
                    y = searchValue;
                    if (Vectors.GetIntersectionPointInLine(new Vector2(aPoint.x, aPoint.y), new Vector2(bPoint.x, bPoint.y), new Vector2(aPoint.x, searchValue), new Vector2(bPoint.x, searchValue), true, out intersectionPoint))
                    {
                        x = intersectionPoint.x;
                        if (Helpers.Vectors.GetIntersectionPointInLine(new Vector2(aPoint.z, aPoint.y), new Vector2(bPoint.z, bPoint.y), new Vector2(aPoint.z, searchValue), new Vector2(bPoint.z, searchValue), true, out intersectionPoint))
                        {
                            z = intersectionPoint.x;
                        }
                    }
                    else
                    {
                        x = aPoint.x;
                        z = aPoint.z;
                    }
                }
            }
            else if (searchPattern == Vector3.right)
            {
                if (Vectors.IsValuesInSpace(aPoint.x, bPoint.x, searchValue, searchValue))
                {
                    x = searchValue;
                    if (Vectors.GetIntersectionPointInLine(new Vector2(aPoint.z, aPoint.x), new Vector2(bPoint.z, bPoint.x), new Vector2(aPoint.z, searchValue), new Vector2(bPoint.z, searchValue), true, out intersectionPoint))
                    {
                        z = intersectionPoint.x;
                        if (Vectors.GetIntersectionPointInLine(new Vector2(aPoint.y, aPoint.x), new Vector2(bPoint.y, bPoint.x), new Vector2(aPoint.y, searchValue), new Vector2(bPoint.y, searchValue), true, out intersectionPoint))
                        {
                            y = intersectionPoint.x;
                        }
                    }
                    else
                    {
                        z = aPoint.z;
                        y = aPoint.y;
                    }
                }
            }
            else if (searchPattern == Vector3.forward)
            {
                if (Vectors.IsValuesInSpace(aPoint.z, bPoint.z, searchValue, searchValue))
                {
                    z = searchValue;
                    if (Vectors.GetIntersectionPointInLine(new Vector2(aPoint.y, aPoint.z), new Vector2(bPoint.y, bPoint.z), new Vector2(aPoint.y, searchValue), new Vector2(bPoint.y, searchValue), true, out intersectionPoint))
                    {
                        y = intersectionPoint.x;
                        if (Vectors.GetIntersectionPointInLine(new Vector2(aPoint.x, aPoint.z), new Vector2(bPoint.x, bPoint.z), new Vector2(aPoint.x, searchValue), new Vector2(bPoint.x, searchValue), true, out intersectionPoint))
                        {
                            x = intersectionPoint.x;
                        }
                    }
                    else
                    {
                        x = aPoint.x;
                        y = aPoint.y;
                    }
                }
            }

            return new Vector3(x, y, z);
        }

        public static List<Vector2> GetHalfLoopPoints(Mesh mesh, float min, float max, float sensitve, Vector3 dir)
        {
            List<Vector2> verticalPoints = new List<Vector2>();
            int count = (int)Mathf.Abs((min - max) / sensitve) + 1;
            float height = min;
            int triangleCount = mesh.triangles.Length / 3;

            for (int j = 0; j < count; j++)
            {
                for (int i = 0; i < triangleCount; i++)
                {
                    Vector3[] points = Helpers.Meshs.GetABEdge(mesh, i);
                    Vector3 point = (Helpers.Meshs.GetIntersectionPoint(points[0], points[1], height, dir));
                    if (point != Vector3.zero)
                    {
                        verticalPoints.Add(new Vector2(Vector2.Distance(Vector2.zero, new Vector2(point.x, point.z)), height));
                        break;
                    }

                    points = Helpers.Meshs.GetBCEdge(mesh, i);
                    point = (Helpers.Meshs.GetIntersectionPoint(points[0], points[1], height, dir));
                    if (point != Vector3.zero)
                    {
                        verticalPoints.Add(new Vector2(Vector2.Distance(Vector2.zero, new Vector2(point.x, point.z)), height));
                        break;
                    }

                    points = Helpers.Meshs.GetCAEdge(mesh, i);
                    point = (Helpers.Meshs.GetIntersectionPoint(points[0], points[1], height, dir));
                    if (point != Vector3.zero)
                    {
                        verticalPoints.Add(new Vector2(Vector2.Distance(Vector2.zero, new Vector2(point.x, point.z)), height));
                        break;
                    }
                }

                height += sensitve;
                height = (float)System.Math.Round(height, 3);
            }

            return verticalPoints;
        }
    }

    public static class Physic
    {
        public static RaycastHit GetScreenToRaycastHit(Camera camera, Vector3 screenPos)
        {
            Ray ray = camera.ScreenPointToRay(screenPos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);

            return hit;
        }

        public static RaycastHit GetRaycastHit(Vector3 startPoint, Vector3 direction)
        {
            Ray ray = new Ray(startPoint, direction);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            return hit;
        }

        public static RaycastHit GetScreenToRaycastHit(Camera camera, Vector3 screenPos, float maxdistance)
        {
            Ray ray = camera.ScreenPointToRay(screenPos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, maxdistance);

            return hit;
        }

        public static RaycastHit GetScreenToRaycastHit(Camera camera, Vector3 screenPos, float maxdistance, LayerMask layer)
        {
            Ray ray = camera.ScreenPointToRay(screenPos);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, maxdistance, layer);

            return hit;
        }

        public static Vector3 CannonBallisticVelocity(Vector3 startPos, Vector3 destination, float angle)
        {
            Vector3 dir = destination - startPos; // get Target Direction
            float height = dir.y; // get height difference
            dir.y = 0; // retain only the horizontal difference
            float dist = dir.magnitude; // get horizontal direction
            float a = angle * Mathf.Deg2Rad; // Convert angle to radians
            dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
            dist += height / Mathf.Tan(a); // Correction for small height differences

            // Calculate the velocity magnitude
            float velocity = Mathf.Sqrt(dist * UnityEngine.Physics.gravity.magnitude / Mathf.Sin(2 * a));
            return velocity * dir.normalized; // Return a normalized vector.
        }

        public static Vector3 CannonBallisticVelocity(Vector3 startPos, Vector3 destination, float angle, float gravity)
        {
            Vector3 dir = destination - startPos; // get Target Direction
            float height = dir.y; // get height difference
            dir.y = 0; // retain only the horizontal difference
            float dist = dir.magnitude; // get horizontal direction
            float a = angle * Mathf.Deg2Rad; // Convert angle to radians
            dir.y = dist * Mathf.Tan(a); // set dir to the elevation angle.
            dist += height / Mathf.Tan(a); // Correction for small height differences

            // Calculate the velocity magnitude
            float velocity = Mathf.Sqrt(dist * gravity / Mathf.Sin(2 * a));
            return velocity * dir.normalized; // Return a normalized vector.
        }

        public static Vector3 VelocityUpdate(Transform transform, Vector3 velocity, Vector3 gravity, float time)
        {
            velocity += gravity.normalized;
            return ((transform.position + velocity) / time).IsNan(Vector3.zero);
        }
    }

    public static class Effects
    {
        public static bool Timer(float timer, float duration, System.Action<float> onTimerUpdateWithPercent = null)
        {
            if (timer < duration)
            {
                timer += Time.deltaTime;
                onTimerUpdateWithPercent?.Invoke(timer / duration);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool Timer(float timer, float duration, float timerSpeed, System.Action<float> onTimerUpdateWithPercent = null)
        {
            if (timer < duration)
            {
                timer += timerSpeed;
                onTimerUpdateWithPercent?.Invoke(timer / duration);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ReverseTimer(float timer, float targetTime, System.Action<float> onTimerUpdateWithRemaning = null)
        {
            if (timer > targetTime)
            {
                timer -= Time.deltaTime;
                onTimerUpdateWithRemaning?.Invoke(timer - targetTime);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool ReverseTimer(float timer, float targetTime, float timerSpeed, System.Action<float> onTimerUpdateWithRemaning = null)
        {
            if (timer > targetTime)
            {
                timer -= timerSpeed;
                onTimerUpdateWithRemaning?.Invoke(timer - targetTime);
                return false;
            }
            else
            {
                return true;
            }
        }

        public static Vector3 Shake(float shakeValue)
        {
            return Random.insideUnitSphere * shakeValue * Time.deltaTime;
        }

        public static void DrawProjectile(LineRenderer line, int vertCount, Vector3 startPoint, Vector3 endPoint)
        {
            int verts = vertCount;
            line.positionCount = verts;
            Vector3 pos = startPoint;
            Vector3 vel = endPoint;
            for (int i = 0; i < verts; i++)
            {
                line.SetPosition(i, pos.IsNan(startPoint));
                vel = vel + UnityEngine.Physics.gravity * Time.fixedDeltaTime;
                pos = pos + vel * Time.fixedDeltaTime;
            }
        }
    }

}
