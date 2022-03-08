using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using DataAccessLayer.Interfaces;
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
           return (PositionDto)position;
        }

        public List<PositionDto> GetAllPositions()
        {
            var positionsList = positionRepository_.GetAllPositions();
            return positionsList.ConvertAll(
                new Converter<IPosition, PositionDto>(IPositionToPositionDto));
        }

        public void AddNewPosition(PositionDto position)
        {
            positionRepository_.AddNewPosition(position);
        }

        public void DeletePositionByDto(PositionDto position)
        {
            positionRepository_.DeletePositionByDto(position);
        }

        public void UpdatePositionByDto(PositionDto position)
        {
            positionRepository_.UpdatePositionByDto(position);
        }

        private static PositionDto IPositionToPositionDto(IPosition address)
        {
            return (PositionDto)address;
        }
    }
}
