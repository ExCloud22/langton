using System;
using System.Collections.Generic;
using Mars.Interfaces.Agents;
using Mars.Interfaces.Annotations;
using Mars.Interfaces.Environments;

namespace GridBlueprint.Model;

public class AntAgent : IAgent<GridLayer>, IPositionable
{
    public void Init(GridLayer layer)
    {
        _layer = layer;
        Position = new Position(StartX, StartY);
        
        _directions = new List<Position>()
        {
            MovementDirections.East,
            MovementDirections.South,
            MovementDirections.West,
            MovementDirections.North
        };
        
        _layer.AntAgentEnvironment.Insert(this);
    }

    public void Tick()
    {
        // 1 == at white square; 0 == at black square
        if (_layer[Position] == 1)
        {
            TurnClockwise();
        } else if (_layer[Position] == 0)
        {
            TurnCounterClockwise();
        }
        
    }

    /// <summary>
    ///     Performs one clockwise turn, if the ant at a white square
    /// </summary>
    private void TurnClockwise()
    {
        _layer[Position] = 0;
        _dir = _dir + 1;
        _currentDirection = _directions[(_dir) % 4];
        MakeAStep(_currentDirection);
    }
    
    /// <summary>
    ///     Performs one counter clockwise turn, if the ant at a black square
    /// </summary>
    private void TurnCounterClockwise()
    {
        _layer[Position] = 1;
        _dir = _dir + 3;
        _currentDirection = _directions[(_dir) % 4];
        MakeAStep(_currentDirection);
    }

    private void MakeAStep(Position direction)
    {
        var newX = Position.X + direction.X;
        var newY = Position.Y + direction.Y;
        Position = new Position(newX, newY);
        _layer.AntAgentEnvironment.MoveTo(this, Position);
    }
    
    [PropertyDescription(Name = "StartX")]
    public int StartX { get; set; }
    
    [PropertyDescription(Name = "StartY")]
    public int StartY { get; set; }
    
    private int _dir = 0;
    private Position _currentDirection = MovementDirections.East;
    private List<Position> _directions;
    private GridLayer _layer;
    public Guid ID { get; set; }
    public Position Position { get; set; }
}