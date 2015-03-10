using UnityEngine;
using System;

// TODO: Rename Matrix2x3
public class SVGMatrix {
    public float a, b, c, d, e, f;
    public SVGMatrix() : this(1, 0, 0, 1, 0, 0) {}
    public SVGMatrix(float a, float b, float c, float d, float e, float f) {
      this.a = a;
      this.b = b;
      this.c = c;
      this.d = d;
      this.e = e;
      this.f = f;
    }

    //---------------------------------------
    public SVGMatrix Multiply(SVGMatrix secondMatrix) {
      float sa, sb, sc, sd, se, sf;
      sa = secondMatrix.a;
      sb = secondMatrix.b;
      sc = secondMatrix.c;
      sd = secondMatrix.d;
      se = secondMatrix.e;
      sf = secondMatrix.f;
      return new SVGMatrix(a*sa + c*sb,     b*sa + d*sb,
                           a*sc + c*sd,     b*sc + d*sd,
                           a*se + c*sf + e, b*se + d*sf + f);
    }
    public SVGMatrix Inverse() {
      double det = a*d - c*b;
      if(det == 0.0) {
        throw new SVGException(SVGExceptionType.MatrixNotInvertable);
      }
      return new SVGMatrix((float)(d/det),            (float)(-b/det),
                           (float)(-c/det),           (float)(a/det),
                           (float)((c*f - e*d)/ det), (float)((e*b - a*f)/ det));
    }
    public SVGMatrix Scale(float scaleFactor) {
      return new SVGMatrix(a * scaleFactor, b * scaleFactor,
                           c * scaleFactor, d * scaleFactor,
                           e,               f);
    }
    public SVGMatrix ScaleNonUniform(float scaleFactorX, float scaleFactorY) {
      return new SVGMatrix(a*scaleFactorX, b*scaleFactorX,
                           c*scaleFactorY, d*scaleFactorY,
                           e,              f);
    }
    public SVGMatrix Rotate(float angle) {
      float ca = Mathf.Cos(angle * Mathf.Deg2Rad);
      float sa = Mathf.Sin(angle * Mathf.Deg2Rad);

      return new SVGMatrix((float)(a*ca + c*sa), (float)(b*ca + d*sa),
                           (float)(c*ca - a*sa), (float)(d*ca - b*sa),
                           e,                    f);
    }
    public SVGMatrix Translate(float x, float y) {
      return new SVGMatrix(a,             b,
                           c,             d,
                           a*x + c*y + e, b*x + d*y + f);
    }
    public SVGMatrix SkewX(float angle) {
      float ta = Mathf.Tan(angle * Mathf.Deg2Rad);
      return new SVGMatrix(a,                 b,
                           (float)(c + a*ta), (float)(d + b*ta),
                           e,                 f);
    }
    public SVGMatrix SkewY(float angle) {
      float ta = Mathf.Tan(angle * Mathf.Deg2Rad);
      return new SVGMatrix((float)(a + c*ta), (float)(b + d*ta),
                           c,                 d,
                           e,                 f);
    }

  public Vector2 Transform(Vector2 point) {
    return new Vector2(a*point.x + c*point.y + e, b*point.x + d*point.y +f);
  }
}
