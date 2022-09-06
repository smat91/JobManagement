using System;
using System.Collections.Generic;
using BusinessLayer.DataTransferObjects;
using BusinessLayer.Interfaces;
using DataAccessLayer.Interfaces;

namespace BusinessLayer.DataAccessConnection
{
    public class Position : IPositionConnection
    {
        private readonly IPositionRepository positionRepository_;

        public Position(IPositionRepository positionRepository)
        {
            positionRepository_ = positionRepository;
        }

        public PositionDto GetSingleById(int id)
        {
           var position = positionRepository_.GetSingleById(id);
           return new PositionDto(position);
        }

        public List<PositionDto> GetBySearchTerm(string searchTerm)
        {
            var positionsList = positionRepository_.GetBySearchTerm(searchTerm);
            return PositionDto.PositionListToPositionDtoList(positionsList);
        }

        public List<PositionDto> GetAll()
        {
            var positionsList = positionRepository_.GetAll();
            return PositionDto.PositionListToPositionDtoList(positionsList);
        }

        public void Add(PositionDto position)
        {
            positionRepository_.Add(PositionDto.PositionDtoToPosition(position));
        }

        public string Delete(PositionDto position)
        {
            return positionRepository_.Delete(PositionDto.PositionDtoToPosition(position));
        }

        public void Update(PositionDto position)
        {
            positionRepository_.Update(PositionDto.PositionDtoToPosition(position));
        }
    }
}
