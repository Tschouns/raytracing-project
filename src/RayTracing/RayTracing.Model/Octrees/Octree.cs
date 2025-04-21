using RayTracing.Base;
using RayTracing.Math;

namespace RayTracing.Model.Octrees
{
    public class Octree
    {
        public Octree(
            AxisAlignedBoundingBox boundingBox,
            IEnumerable<Face> allFaces,
            IEnumerable<Octree> children)
        {
            Argument.AssertNotNull(boundingBox, nameof(boundingBox));
            Argument.AssertNotNull(allFaces, nameof(allFaces));
            Argument.AssertNotNull(children, nameof(children));

            this.BoundingBox = boundingBox;
            this.AllFaces = allFaces;
            this.Children = children;
        }

        public AxisAlignedBoundingBox BoundingBox { get; }
        IEnumerable<Face> AllFaces { get; }
        public IEnumerable<Octree> Children { get; }
        public bool HasChildren => this.Children.Any();
    }
}
