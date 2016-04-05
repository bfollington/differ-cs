using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using Differ;
using Differ.Shapes;

public class CircleCollisionTest {

    [Test]
    public void EditorTest()
    {
        //Arrange
        var circle = new Circle(0, 0, 5);
		var anotherCircle = new Circle(3, 0, 3);

        //Act
        //Try to rename the GameObject
        var collision = Differ.Collision.shapeWithShape(circle, anotherCircle);

        //Assert
        //The object has a new name
        Assert.AreEqual(5, collision.overlap);
		Assert.AreEqual(-5, collision.separationX);
		Assert.AreSame(collision.shape1, circle);
		Assert.AreSame(collision.shape2, anotherCircle);
    }
}
