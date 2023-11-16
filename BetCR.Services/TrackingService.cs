using System;
using System.Threading;
using System.Threading.Tasks;
using BetCR.Library.Tracking.Infrastructure;
using BetCR.Repository;
using BetCR.Repository.Entity;
using BetCR.Repository.Repository.Base;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace BetCR.Services
{
    public class TrackingService : BackgroundService
    {
        #region Private Fields

        private readonly ISubscriber _subscriber;

        #endregion Private Fields

        #region Public Constructors

        private static JsonSerializerSettings _JsonSerializerSettings = new() {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};

        public TrackingService(ISubscriber subscriber)
        {
            _subscriber = subscriber;
        }

        #endregion Public Constructors

        #region Public Methods

        private void BeginTracking()
        {
            _subscriber.Subscribe();
            _subscriber.MessageReceived += async (sender, e) =>
            {
                switch (e)
                {
                    case EntityChangeModel entityChangeModel:

                        var entityId = entityChangeModel.EntityId;
                        var changeEvent = new ChangeEvent
                        {
                            Id = Guid.NewGuid().ToString("D"),
                            DataType = entityChangeModel.Entity.GetType().FullName,
                            EntityId = entityId,
                            StreamId = entityId,
                            EventType = entityChangeModel.EventType
                        };

                        changeEvent.Data = JsonConvert.SerializeObject(entityChangeModel.Entity, _JsonSerializerSettings);
                        var context = entityChangeModel.Context;
                        var repository = new BaseRepository<ChangeEvent, string>(context);
                        await repository.AddAsync(changeEvent);
                        await context.SaveChangesAsync();
                        break;
                }
            };
        }

        #endregion Public Methods

        #region Public Methods

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            BeginTracking();
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested) await base.StopAsync(cancellationToken);
        }

        #endregion Public Methods

        #region Protected Methods

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested) await Task.Delay(2000, stoppingToken);

            if (!stoppingToken.IsCancellationRequested) Console.WriteLine("Stopping");
        }

        #endregion Protected Methods
    }
}