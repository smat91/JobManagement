using System.Collections.Generic;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLayer.DataTransferObjects
{
    public class PositionDto
    {
        public int Id { get; set; }
        public ItemDto Item { get; set; }
        public int Amount { get; set; }

        public PositionDto()
        {
        }

        public PositionDto(IPosition position)
        {
            Id = position.Id;
            Item = new ItemDto(position.Item);
            Amount = position.Amount;
        }

        public static DataAccessLayer.Models.Position PositionDtoToPosition(PositionDto position)
        {
            return new DataAccessLayer.Models.Position
            {
                Id = position.Id,
                Item = ItemDto.ItemDtoToItem(position.Item),
                Amount = position.Amount,
            };
        }
        
        public static List<PositionDto> PositionListToPositionDtoList(List<IPosition> positions)
        {
            List<PositionDto> positionDtos = new List<PositionDto>();
            foreach (var position in positions)
            {
                positionDtos.Add(new PositionDto(position));
            }

            return positionDtos;
        }

        public static ICollection<PositionDto> PositionCollectionToPositionDtoCollection(ICollection<Position> positions)
        {
            ICollection<PositionDto> positionDtos = new List<PositionDto>();
            foreach (var position in positions)
            {
                positionDtos.Add(new PositionDto(position));
            }

            return positionDtos;
        }

        public static ICollection<Position> PositionDtoCollectionToPositionCollection(ICollection<PositionDto> positionDtos)
        {
            ICollection<Position> positions = new List<Position>();
            foreach (var position in positionDtos)
            {
                positions.Add( PositionDtoToPosition(position));
            }

            return positions;
        }
    }
}
