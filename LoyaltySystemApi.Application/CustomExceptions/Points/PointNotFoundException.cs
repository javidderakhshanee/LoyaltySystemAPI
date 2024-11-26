namespace LoyaltySystemApi.Application.CustomExceptions.Points;

public sealed class PointNotFoundException:ApplicationException
{
    public PointNotFoundException():base("Point not found!")
    {
            
    }
}
