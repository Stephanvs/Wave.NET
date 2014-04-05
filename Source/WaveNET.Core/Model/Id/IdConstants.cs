namespace WaveNET.Core.Model.Id
{
    /// <summary>
    ///     Constants useful for the context of ids.
    /// </summary>
    public static class IdConstants
    {
        /// <summary>
        ///     Conventional separator for tokens in an id.
        /// </summary>
        public const string TokenSeparator = "+";

        /// <summary>
        ///     Scheme for wave and wavelet URIs.
        /// </summary>
        public const string WaveUriScheme = "wave";

        /// <summary>
        ///     Prefix for conversational wave ids.
        /// </summary>
        public const string WavePrefix = "w";

        /// <summary>
        ///     Prefix for profile wave ids.
        /// </summary>
        public const string ProfileWavePrefix = "prof";

        /// <summary>
        ///     Prefix for conversational wavelet ids.
        /// </summary>
        public const string ConversationWaveletPrefix = "conv";

        /// <summary>
        ///     Prefix for blip document ids.
        /// </summary>
        public const string BlipPrefix = "b";

        /// <summary>
        ///     Name of the data document that contains tag information.
        /// </summary>
        public const string TagsDocId = "tags";

        /// <summary>
        ///     Prefix for ghost blip document ids. Ghost blips aren't rendered.
        /// </summary>
        public const string GhostBlipPrefix = "g";

        /// <summary>
        ///     Conventional converstation root wavelet id.
        /// </summary>
        public const string ConversationRootWavelet =
            ConversationWaveletPrefix + TokenSeparator + "root";

        /// <summary>
        ///     Prefix of the name of the attachment metadata data document.
        /// </summary>
        public const string AttachmentMetadataPrefix = "attach";

        /// <summary>
        ///     Old metadata document id. // Todo: remove
        /// </summary>
        public const string OldMetadataDocId = "m/metadata";
    }
}