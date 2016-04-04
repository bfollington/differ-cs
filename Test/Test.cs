using UnityEngine;
using System.Collections;
using Differ;
using Differ.Shapes;

public class Test {

	public static void TestStuff() {
		var circle = new Circle(0, 0, 5);
		var anotherCircle = new Circle(3, 0, 3);

		var res = Differ.Collision.shapeWithShape(circle, anotherCircle);
		Debug.Log(":)");
	}
}
