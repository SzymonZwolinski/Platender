﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platender.Core.Extensions.EnumExtensions
{
    public static class EnumExtension
    {
        public static TEnum? MapStringToEnumOrNull<TEnum>(this string input) where TEnum : struct, Enum
        {
            if (Enum.TryParse<TEnum>(input, out TEnum result))
            {
                return result;
            }

            return null;
        }
    }
}
