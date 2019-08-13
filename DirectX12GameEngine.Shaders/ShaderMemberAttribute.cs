﻿using System;
using System.Runtime.CompilerServices;

namespace DirectX12GameEngine.Shaders
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public class ShaderContractAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class IgnoreShaderMemberAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ShaderMemberAttribute : Attribute
    {
        public ShaderMemberAttribute([CallerLineNumber] int order = 0)
        {
            Order = order;
        }

        public int Order { get; }

        public bool Override { get; set; }
    }

    public class ConstantBufferResourceAttribute : ShaderMemberAttribute
    {
        public ConstantBufferResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    public class SamplerResourceAttribute : ShaderMemberAttribute
    {
        public SamplerResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    public class TextureResourceAttribute : ShaderMemberAttribute
    {
        public TextureResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    public class UnorderedAccessViewResourceAttribute : ShaderMemberAttribute
    {
        public UnorderedAccessViewResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    public class StaticResourceAttribute : ShaderMemberAttribute
    {
        public StaticResourceAttribute([CallerLineNumber] int order = 0) : base(order)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class NumThreadsAttribute : Attribute
    {
        public NumThreadsAttribute(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public int X { get; }

        public int Y { get; }

        public int Z { get; }
    }
}