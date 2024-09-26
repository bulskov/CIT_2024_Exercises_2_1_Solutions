using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2_1_3
{
    public class FunctionalDependency
    {
        public FunctionalDependency(string lhs, string rhs)
        {
            LeftHandSide = new AttributeList(lhs);
            RightHandSide = new AttributeList(rhs);
        }

        public AttributeList LeftHandSide { get; }
        public AttributeList RightHandSide { get; }

        public override string ToString()
        {
            return $"{{{LeftHandSide}}} -> {{{RightHandSide}}}";
        }
    }
}
