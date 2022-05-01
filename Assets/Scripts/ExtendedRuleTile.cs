using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "Extended Rule Tile", menuName = "2D/Tiles/Extended Rule Tile")]
public class ExtendedRuleTile : RuleTile
{

    public string type;
    public override bool RuleMatch(int neighbor, TileBase other)
    {
        if (other is RuleOverrideTile)
            other = (other as RuleOverrideTile).m_InstanceTile;

        ExtendedRuleTile otherTile = other as ExtendedRuleTile;

        if (otherTile == null)
            return base.RuleMatch(neighbor, other);

        if (!(type == type.ToLower()))
        {
            switch (neighbor)
            {
                case TilingRule.Neighbor.This: return type.ToLower() == otherTile.type.ToLower();
                case TilingRule.Neighbor.NotThis: return type.ToLower() != otherTile.type.ToLower();
            }
        }
        else
        {
            switch (neighbor)
            {
                case TilingRule.Neighbor.This: return type == otherTile.type;
                case TilingRule.Neighbor.NotThis: return type != otherTile.type;
            }
        }
        return true;

    }
}