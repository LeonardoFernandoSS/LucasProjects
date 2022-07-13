using UnityEngine;
using System.Collections;

/**
 * This class is used to create polynomial curves
 **/
public class Bezier {

	/**
	 * This method calculates the curves.
	 * Minimal requirements is the firstPoint, lastPoint and t. The controlPoints is used according required curve.
	 * 
	 * 
	 * firstPoint = It's the initial transform position.
	 * 
	 * lastPoint = It's the final transform position.
	 * 
	 * controlPoints = It's the control transforms positions.
	 * 
	 * t = It's a parameterization to traverse the curve and ranges [0.0 - 1.0].
	 * 
	 **/
	public static Vector3 calc(Transform firstPoint, Transform[] controlPoints, Transform lastPoint, float t){

		// indexes to calculate
		int _mainExponent = controlPoints.Length + 1, 
			_i = 1;

		float _const = 1 - t; // like "(1 - t)"

		Vector3 _fPoint = Mathf.Pow(_const, _mainExponent) * firstPoint.position, // like "(1 - t) * Pi" or "(1 - t)² * Pi"     -->     Pi = Ponto inicial
				_lPoint = Mathf.Pow(t, _mainExponent) * lastPoint.position, // like "t * Pf" or "t² * Pf"     -->     Pf = Ponto final
				_sumCPoint = Vector3.zero;
	
		// if has controlPoints, then create a sum of another formula
		foreach(Transform cPoint in controlPoints){

			// like "2t(1 - t) * Pc" or "3t(1 - t)² * Pc1 + 3t²(1 - t) * Pc2"     -->     Pc = Ponto de controle
			_sumCPoint += (_mainExponent * Mathf.Pow(t, _i)) * 
						  Mathf.Pow(_const, (controlPoints.Length + 1) - _i) * 
						  cPoint.position;
			
			_i++;

		}

		// returns the sum of the three equations
		return _fPoint + _sumCPoint + _lPoint;

	}

}