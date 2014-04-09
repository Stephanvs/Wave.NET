namespace WaveNET.Core.Model.Operation.Wave
{
    public interface IBlipOperationVisitor 
        : IVisitor
    {
        void VisitBlipContentOperation(BlipContentOperation op);
        
        //void VisitSubmitBlip(SubmitBlip op);
    }
}