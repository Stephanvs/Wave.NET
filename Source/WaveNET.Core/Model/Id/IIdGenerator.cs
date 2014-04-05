namespace WaveNET.Core.Model.Id
{
    /// <summary>
    ///     This class is used to generate Wave and Wavelet ids.
    ///     The id field is structured as a sequence of '+'-separated tokens.
    ///     The id field is case sensitive.
    ///     A wavelet is hosted by a single wave service provider, which may differ from
    ///     the service provider which allocated the wave id. Common examples are private
    ///     replies and user-data wavelets for users from a federated domain. Thus, the
    ///     service provider specified in a wavelet id may differ from the service
    ///     provider of the wave to which it belongs.
    /// </summary>
    public interface IIdGenerator
    {
        /// <summary>
        ///     Creates a new unique wave id.
        /// </summary>
        /// <returns>
        ///     Conversational waves (all the waves we have today) are specified by a leading
        ///     token 'w' followed by a pseudo-random string, e.g. w+3dKS9cD.
        /// </returns>
        WaveId NewWaveId();

        /// <summary>
        ///     Creates a new unique wavelet id.
        /// </summary>
        /// <returns>
        ///     Conversational wavelets (those expected to render in a wave client) are
        ///     specified by a leading token "conv" followed by a psuedorandom string,
        ///     e.g. conv+3sG7. The distinguished root conversation wavelet is
        /// </returns>
        WaveletId NewConversationWaveletId();

        /// <summary>
        ///     Create a new conversation root wavelet id.
        /// </summary>
        /// <returns>The conversation root wavelet has id "conv+root"</returns>
        WaveletId NewConversationRootWaveletId();
    }
}