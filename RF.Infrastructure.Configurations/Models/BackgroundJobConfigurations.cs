namespace RF.Infrastructure.Configurations.Models
{
    public sealed class BackgroundJobConfigurations : BaseConfigurations
    {
        /// <summary>
        /// Gets or sets the number of workers available for background processing.
        /// </summary>
        /// <value>
        /// The number of workers.
        /// </value>
        public int NumberOfWorkers { get; set; }

        /// <summary>
        /// Gets or sets the name of the server.
        /// </summary>
        /// <value>
        /// The name of the server.
        /// </value>
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or sets the database schema.
        /// </summary>
        /// <value>
        /// The database schema.
        /// </value>
        public string DatabaseSchema { get; set; }

        /// <summary>
        /// Gets or sets the database tags schema.
        /// </summary>
        /// <value>
        /// The database tags schema.
        /// </value>
        public string DatabaseTagsSchema { get; set; }
    }
}