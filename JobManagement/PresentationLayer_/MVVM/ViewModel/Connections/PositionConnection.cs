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
        private readonly PositionRepository positionRepository_;

        public PositionConnection(string connectionString)
        {
            positionRepository_ = new PositionRepository(connectionString);
        }

        public IPosition GetPositionById(int id)
        {
           var position = positionRepository_.GetPositionById(id);
           return position;
        }

        public void AddNewPosition(IPosition position)
        {
            positionRepository_.AddNewPosition(position);
        }

        public void DeletePositionByDto(IPosition position)
        {
           positionRepository_.DeletePositionByDto(position);
        }

        public void UpdatePositionByDto(IPosition position)
        {
           positionRepository_.UpdatePositionByDto(position);
        }
    }
}
