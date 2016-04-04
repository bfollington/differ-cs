using System;
using Differ.Data;
using Differ.Sat;

namespace Differ.Shapes
{
	public class Circle : Shape
	{
		public float radius { get; private set; }
		public float transformedRadius { get { return radius * scaleX; } private set; }

		public Circle (float x, float y, float radius) : base(x, y)
		{
			this.radius = radius;
			name = "circle " + radius;
		}

		public override ShapeCollision test (Shape shape)
		{
			return shape.testCircle(this, true);
		}

		public override ShapeCollision testCircle (Circle circle, bool flip = false)
		{
			return Sat2D.testCircleVsCircle(this, circle, flip);
		}

		public override ShapeCollision testPolygon (Polygon polygon, bool flip = false)
		{
			return Sat2D.testCircleVsPolygon( this, polygon, flip );
		}

		public override RayCollision testRay (UnityEngine.Ray ray)
		{
			return Sat2D.testRayVsCircle(ray, this);
		}
	}
}