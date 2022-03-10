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
           var position = positionRepository_.GetPositionById(id);
           return new PositionDto(position);
        }

        public List<PositionDto> GetPositionsBySearchTerm(string searchTerm)
        {
            var positionsList = positionRepository_.GetCustomersBySearchTerm(searchTerm);
            return PositionDto.PositionListToPositionDtoList(positionsList);
        }

        public List<PositionDto> GetAllPositions()
        {
            var positionsList = positionRepository_.GetAllPositions();
            return PositionDto.PositionListToPositionDtoList(positionsList);
        }

        public void AddNewPosition(PositionDto position)
        {
            positionRepository_.AddNewPosition(PositionDto.PositionDtoToPosition(position));
        }

        public void DeletePositionByDto(PositionDto position)
        {
            positionRepository_.DeletePositionByDto(PositionDto.PositionDtoToPosition(position));
        }

        public void UpdatePositionByDto(PositionDto position)
        {
            positionRepository_.UpdatePositionByDto(PositionDto.PositionDtoToPosition(position));
        }
    }
}
