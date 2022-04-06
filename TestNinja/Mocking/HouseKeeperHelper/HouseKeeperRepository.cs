using System.Linq;

namespace TestNinja.Mocking.HouseKeeperHelper;

public interface IHouseKeeperRepository
{
    IQueryable<Housekeeper> GetHouseKeepers();
}

public class HouseKeeperRepository : IHouseKeeperRepository
{
    public IQueryable<Housekeeper> GetHouseKeepers()
    {
        var unitOfWork = new UnitOfWork();
        var houseKeepers =
            unitOfWork.Query<Housekeeper>();

        return houseKeepers;
    }
}