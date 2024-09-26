using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercise_2_1_3
{
    public class AttributeClosure
    {
        public static AttributeList ComputeClosure(
            AttributeList attr, List<FunctionalDependency> fdList)
        {

            var size = 0;
            var result = new AttributeList(attr);
            do
            {
                size = result.Count;
                foreach (var fd in fdList)
                {
                    if (result.Contains(fd.LeftHandSide))
                    {
                        result.Add(fd.RightHandSide);
                    }
                }
            } while (size != result.Count);

            return result;
        }

        public static List<AttributeList> FindSuperKeys(
            AttributeList relation, List<FunctionalDependency> fdList)
        {
            var result = new List<AttributeList>();
            var combinations = GetCombinations(relation);
            foreach (var attrList in combinations)
            {
                var closure = ComputeClosure(attrList, fdList);
                if (closure.Count >= relation.Count)
                {
                    result.Add(attrList);
                }
            }
            return result;
        }

        public static List<AttributeList> FindKeys(AttributeList relation, List<FunctionalDependency> fdList)
        {
            var superKeys = FindSuperKeys(relation, fdList);

            var result = new List<AttributeList>();

            foreach (var superKey in superKeys)
            {
                if (!superKeys.HasSubSetOf(superKey))
                    result.Add(superKey);
            }
            return result;
        }

        public static List<AttributeList> GetCombinations(AttributeList list)
        {
            var result = new List<AttributeList>();
            var count = (int)Math.Pow(2, list.Count);
            for (var i = 1; i <= count - 1; i++)
            {
                string str = Convert.ToString(i, 2).PadLeft(list.Count, '0');
                var combi = new AttributeList();
                for (var j = 0; j < str.Length; j++)
                {
                    if (str[j] == '1')
                    {
                        combi.Add(list[j]);
                    }
                }
                result.Add(combi);
            }
            return result;
        }
    }
}
