using System.Collections.Generic;
using UnityEngine;

namespace Controllers
{

    public class AmmoContaynerGridController 
    {

        private int _orderOfContayner ;
        private float _offset;
        private int _xGridSize;
        private int _yGridSize;
        private int _maxContaynerAmount;
        private bool _contaynerFull;
        private Vector3 _lastPosition;
        private List<Vector3> _contaynerStackGridPositions= new List<Vector3>();

        public AmmoContaynerGridController( int xGridSize, int yGridSize, int maxContaynerAmount, float offset)
        {
            _xGridSize = xGridSize;
            _yGridSize = yGridSize;
            _maxContaynerAmount = maxContaynerAmount;
            _offset = offset;
        }

        public void GanarateGrid()
        {


            for (int i = 0; i < _maxContaynerAmount; i++)
            {
                if (_contaynerFull) return;


                var modx = _orderOfContayner % _xGridSize;

                var dividey = _orderOfContayner / _xGridSize;

                var mody = dividey % _yGridSize;

                var divideXY = _orderOfContayner / (_xGridSize * _yGridSize);

                _lastPosition = new Vector3(modx * _offset, divideXY * _offset, mody * _offset);//List place

                _contaynerStackGridPositions.Add(_lastPosition);

                if (_orderOfContayner == _maxContaynerAmount - 1)
                {
                    _contaynerFull = true;
                }
                else
                {
                    _contaynerFull = false;
                    _orderOfContayner += 1;
                } 
            }

          
        }

        public List<Vector3> LastPosition()
        {
            return _contaynerStackGridPositions;
        }


        
    }
}