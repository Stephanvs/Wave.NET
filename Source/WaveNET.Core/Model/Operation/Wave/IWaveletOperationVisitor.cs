namespace WaveNET.Core.Model.Operation.Wave
{
    public interface IWaveletOperationVisitor
        : IVisitor
    {
        void VisitNoOp(NoOp op);
        void VisitVersionUpdateOperation(VersionUpdateOperation operation);
        void VisitAddParticipantOperation(AddParticipantOperation operation);
        void VisitRemoveParticipantOperation(RemoveParticipantOperation operation);
        void VisitWaveletBlipOperation(WaveletBlipOperation operation);
    }
}