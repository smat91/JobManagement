using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Repositories;

namespace BusinessLayer.DataAccessConnection
{
    public class Position
    {
        private readonly PositionRepository positionRepository_;

        public Position()
        {
            positionRepository_ = new PositionRepository();
        }

        public PositionDto GetPositionById(int id)
        {
           var position = positionRepository_.GetSingleById(id);
           return new PositionDto(position);
        }

        public List<PositionDto> GetPositionsBySearchTerm(string searchTerm)
        {
            var positionsList = positionRepository_.GetBySearchTerm(searchTerm);
            return PositionDto.PositionListToPositionDtoList(positionsList);
        }

        public List<PositionDto> GetAllPositions()
        {
            var positionsList = positionRepository_.GetAll();
            return PositionDto.PositionListToPositionDtoList(positionsList);
        }

        public void AddNewPosition(PositionDto position)
        {
            positionRepository_.Add(PositionDto.PositionDtoToPosition(position));
        }

        public string DeletePositionByDto(PositionDto position)
        {
            return positionRepository_.Delete(PositionDto.PositionDtoToPosition(position));
        }

        public void UpdatePositionByDto(PositionDto position)
        {
            positionRepository_.Update(PositionDto.PositionDtoToPosition(position));
        }
    }
}
