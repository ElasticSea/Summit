using UnityEngine;

namespace Shared.Scripts
{
    public class MathHelper
    {
        // Secant 
        public static float Sec(float x)
        {
            return 1/Mathf.Cos(x);
        }

        // Cosecant
        public static float Csc(float x)
        {
            return Cosec(x);
        }

        // Cosecant
        public static float Cosec(float x)
        {
            return 1/Mathf.Sin(x);
        }

        // Cotangent 
        public static float Cotan(float x)
        {
            return 1/Mathf.Tan(x);
        }

        // Inverse Sine 
        public static float Arcsin(float x)
        {
            return Mathf.Atan(x/Mathf.Sqrt(-x*x + 1));
        }

        // Inverse Cosine 
        public static float Arccos(float x)
        {
            return Mathf.Atan(-x/Mathf.Sqrt(-x*x + 1)) + 2*Mathf.Atan(1);
        }

        // Inverse Secant 
        public static float Arcsec(float x)
        {
            return 2*Mathf.Atan(1) - Mathf.Atan(Mathf.Sign(x)/Mathf.Sqrt(x*x - 1));
        }

        // Inverse Cosecant 
        public static float Arccosec(float x)
        {
            return Mathf.Atan(Mathf.Sign(x)/Mathf.Sqrt(x*x - 1));
        }

        // Inverse Cotangent 
        public static float Arccotan(float x)
        {
            return 2*Mathf.Atan(1) - Mathf.Atan(x);
        }

        // Hyperbolic Sine 
        public static float HSin(float x)
        {
            return (Mathf.Exp(x) - Mathf.Exp(-x))/2;
        }

        // Hyperbolic Cosine 
        public static float HCos(float x)
        {
            return (Mathf.Exp(x) + Mathf.Exp(-x))/2;
        }

        // Hyperbolic Tangent 
        public static float HTan(float x)
        {
            return (Mathf.Exp(x) - Mathf.Exp(-x))/(Mathf.Exp(x) + Mathf.Exp(-x));
        }

        // Hyperbolic Secant 
        public static float HSec(float x)
        {
            return 2/(Mathf.Exp(x) + Mathf.Exp(-x));
        }

        // Hyperbolic Cosecant 
        public static float HCosec(float x)
        {
            return 2/(Mathf.Exp(x) - Mathf.Exp(-x));
        }

        // Hyperbolic Cotangent 
        public static float HCotan(float x)
        {
            return (Mathf.Exp(x) + Mathf.Exp(-x))/(Mathf.Exp(x) - Mathf.Exp(-x));
        }

        // Inverse Hyperbolic Sine 
        public static float HArcsin(float x)
        {
            return Mathf.Log(x + Mathf.Sqrt(x*x + 1));
        }

        // Inverse Hyperbolic Cosine 
        public static float HArccos(float x)
        {
            return Mathf.Log(x + Mathf.Sqrt(x*x - 1));
        }

        // Inverse Hyperbolic Tangent 
        public static float HArctan(float x)
        {
            return Mathf.Log((1 + x)/(1 - x))/2;
        }

        // Inverse Hyperbolic Secant 
        public static float HArcsec(float x)
        {
            return Mathf.Log((Mathf.Sqrt(-x*x + 1) + 1)/x);
        }

        // Inverse Hyperbolic Cosecant 
        public static float HArccosec(float x)
        {
            return Mathf.Log((Mathf.Sign(x)*Mathf.Sqrt(x*x + 1) + 1)/x);
        }

        // Inverse Hyperbolic Cotangent 
        public static float HArccotan(float x)
        {
            return Mathf.Log((x + 1)/(x - 1))/2;
        }

        // Logarithm to base N 
        public static float LogN(float x, float n)
        {
            return Mathf.Log(x)/Mathf.Log(n);
        }
    }
}