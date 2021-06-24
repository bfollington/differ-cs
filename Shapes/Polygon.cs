using System;
using System.Collections.Generic;
using Differ.Data;
using Differ.Math;
using Differ.Sat;
using UnityEngine;

namespace Differ.Shapes
{
    public class Polygon : Shape
    {
        /** The vertices of this shape */
        private Vector[] _vertices
        {
            get
            {
                return _vertices;
            }

            set
            {
                _vertices = value;
                name = "polygon(sides:" + _vertices.Length + ")";
            }
        }
        public Vector[] vertices { get; set; }

        /** The transformed (rotated/scale) vertices cache */
        public Vector[] transformedVertices
        {
            get
            {

                if (!_transformed)
                {
                    var _count = vertices.Length;

                    _transformedVertices = new Vector[_count];
                    _transformed = true;

                    for (var i = 0; i < _count; i++)
                    {
                        _transformedVertices[i] = (vertices[i].transform(_transformMatrix));
                    }
                }

                return _transformedVertices;
            }
        }


        private Vector[] _transformedVertices;

        public Polygon(float x, float y, Vector[] vertices) : base(x, y)
        {
            _transformedVertices = new Vector[vertices.Length];
            this.vertices = vertices;
        }

        public override ShapeCollision test(Shape shape)
        {
            return shape.testPolygon(this, true);
        }

        public override ShapeCollision testCircle(Circle circle, bool flip = false)
        {
            return Sat2D.testCircleVsPolygon(circle, this, !flip);
        }

        public override ShapeCollision testPolygon(Polygon polygon, bool flip = false)
        {
            return Sat2D.testPolygonVsPolygon(this, polygon, flip);
        }

        public override RayCollision testRay(Ray ray)
        {
            return Sat2D.testRayVsPolygon(ray, this);
        }

        /** Helper to create an Ngon at x,y with given number of sides, and radius.
            A default radius of 100 if unspecified. Returns a ready made `Polygon` collision `Shape` */
        public static Polygon create(float x, float y, int sides, float radius)
        {
            if (sides < 3)
            {
                throw new ArgumentException("A polygon must have a least 3 sides.");
            }

            float rotation = (float)(System.Math.PI * 2) / sides;
            float angle;
            Vector vector;
            Vector[] vertices = new Vector[sides];

            for (var i = 0; i < sides; i++)
            {
                angle = (float)((i * rotation) + ((System.Math.PI - rotation) * 0.5));
                vector = new Vector();
                vector.x = (float)System.Math.Cos(angle) * radius;
                vector.y = (float)System.Math.Sin(angle) * radius;
                vertices[i] = (vector);
            }

            return new Polygon(x, y, vertices);
        }

        /** Helper generate a rectangle at x,y with a given width/height and centered state.
            Centered by default. Returns a ready made `Polygon` collision `Shape` */
        public static Polygon rectangle(float x, float y, float width, float height, bool centered = true)
        {

            var vertices = new Vector[4];

            if (centered)
            {

                vertices[0] = (new Vector(-width / 2, -height / 2));
                vertices[1] = (new Vector(width / 2, -height / 2));
                vertices[2] = (new Vector(width / 2, height / 2));
                vertices[3] = (new Vector(-width / 2, height / 2));

            }
            else
            {

                vertices[0] = (new Vector(0, 0));
                vertices[1] = (new Vector(width, 0));
                vertices[2] = (new Vector(width, height));
                vertices[3] = (new Vector(0, height));

            }

            return new Polygon(x, y, vertices);
        }

        /** Helper generate a square at x,y with a given width/height with given centered state.
            Centered by default. Returns a ready made `Polygon` collision `Shape` */
        public static Polygon square(float x, float y, float width, bool centered = true)
        {
            return rectangle(x, y, width, width, centered);
        }

        /** Helper generate a triangle at x,y with a given radius.
            Returns a ready made `Polygon` collision `Shape` */
        public static Polygon triangle(float x, float y, float radius)
        {
            return create(x, y, 3, radius);
        }
    }
}