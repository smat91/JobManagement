using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.MVVM.ViewModel.Connections
{
    public class PositionConnection
    {
        public static IPosition GetPositionById(int id)
        {
           var position = PositionRepository.GetPositionById(id);
           return position;
        }

        public static List<IPosition> GetAllPositions()
        {
            var positionsList = PositionRepository.GetAllPositions();
            return positionsList;
        }

        public static void AddNewPosition(IPosition position)
        {
            PositionRepository.AddNewPosition(position);
        }

        public static void DeletePositionByDto(IPosition position)
        {
            PositionRepository.DeletePositionByDto(position);
        }

        public static void UpdatePositionByDto(IPosition position)
        {
           PositionRepository.UpdatePositionByDto(position);
        }
    }
}
