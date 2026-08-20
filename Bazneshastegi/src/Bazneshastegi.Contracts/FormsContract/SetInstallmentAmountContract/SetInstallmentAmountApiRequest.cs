namespace Bazneshastegi.Contracts.FormsContract.SetInstalementAmountContract;

public readonly record struct SetInstalementAmountApiRequest(
        string RequestTypeID,
        decimal DefaultAmount,
        decimal DefaultInstalementCount);