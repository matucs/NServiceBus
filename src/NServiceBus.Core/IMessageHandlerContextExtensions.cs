namespace NServiceBus
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Syntactic sugar for <see cref="IMessageHandlerContext"/>.
    /// </summary>
    public static class IMessageHandlerContextExtensions
    {
        /// <summary>
        /// Sends the message to the endpoint which sent the message currently being handled on this thread.
        /// </summary>
        /// <param name="context">Object being extended.</param>
        /// <param name="message">The message to send.</param>
        public static Task ReplyAsync(this IMessageHandlerContext context, object message)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNull(nameof(message), message);

            return context.ReplyAsync(message, new ReplyOptions());
        }

        /// <summary>
        /// Instantiates a message of type T and performs a regular ReplyAsync.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="context">Object being extended.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        public static Task ReplyAsync<T>(this IMessageHandlerContext context, Action<T> messageConstructor)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNull(nameof(messageConstructor), messageConstructor);

            return context.ReplyAsync(messageConstructor, new ReplyOptions());
        }

        /// <summary>
        /// Sends the provided message.
        /// </summary>
        /// <param name="context">The instance of <see cref="IMessageHandlerContext"/> to use for the action.</param>
        /// <param name="message">The message to send.</param>
        public static Task SendAsync(this IMessageHandlerContext context, object message)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNull(nameof(message), message);

            return context.SendAsync(message, new SendOptions());
        }

        /// <summary>
        /// Instantiates a message of <typeparamref name="T"/> and sends it.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="context">The instance of <see cref="IMessageHandlerContext"/> to use for the action.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        /// <remarks>
        /// The message will be sent to the destination configured for <typeparamref name="T"/>.
        /// </remarks>
        public static Task SendAsync<T>(this IMessageHandlerContext context, Action<T> messageConstructor)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNull(nameof(messageConstructor), messageConstructor);

            return context.SendAsync(messageConstructor, new SendOptions());
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <param name="context">The instance of <see cref="IMessageHandlerContext"/> to use for the action.</param>
        /// <param name="destination">The address of the destination to which the message will be sent.</param>
        /// <param name="message">The message to send.</param>
        public static Task SendAsync(this IMessageHandlerContext context, string destination, object message)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNullAndEmpty(nameof(destination), destination);
            Guard.AgainstNull(nameof(message), message);

            var options = new SendOptions();

            options.SetDestination(destination);

            return context.SendAsync(message, options);
        }

        /// <summary>
        /// Instantiates a message of type T and sends it to the given destination.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="context">The instance of <see cref="IMessageHandlerContext"/> to use for the action.</param>
        /// <param name="destination">The destination to which the message will be sent.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        public static Task SendAsync<T>(this IMessageHandlerContext context, string destination, Action<T> messageConstructor)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNullAndEmpty(nameof(destination), destination);
            Guard.AgainstNull(nameof(messageConstructor), messageConstructor);

            var options = new SendOptions();

            options.SetDestination(destination);

            return context.SendAsync(messageConstructor, options);
        }

        /// <summary>
        /// Sends the message back to the current bus.
        /// </summary>
        /// <param name="context">Object being extended.</param>
        /// <param name="message">The message to send.</param>
        public static Task SendLocalAsync(this IMessageHandlerContext context, object message)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNull(nameof(message), message);

            var options = new SendOptions();

            options.RouteToLocalEndpointInstance();

            return context.SendAsync(message, options);
        }

        /// <summary>
        /// Instantiates a message of type T and sends it back to the current bus.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="context">Object being extended.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        public static Task SendLocalAsync<T>(this IMessageHandlerContext context, Action<T> messageConstructor)
        {
            Guard.AgainstNull(nameof(context), context);
            Guard.AgainstNull(nameof(messageConstructor), messageConstructor);

            var options = new SendOptions();

            options.RouteToLocalEndpointInstance();

            return context.SendAsync(messageConstructor, options);
        }

        /// <summary>
        /// Publish the message to subscribers.
        /// </summary>
        /// <param name="context">The instance of <see cref="IMessageHandlerContext"/> to use for the action.</param>
        /// <param name="message">The message to publish.</param>
        public static Task PublishAsync(this IMessageHandlerContext context, object message)
        {
            return context.PublishAsync(message, new PublishOptions());
        }

        /// <summary>
        /// Publish the message to subscribers.
        /// </summary>
        /// <param name="context">The instance of <see cref="IMessageHandlerContext"/> to use for the action.</param>
        /// <typeparam name="T">The message type.</typeparam>
        public static Task PublishAsync<T>(this IMessageHandlerContext context)
        {
            return context.PublishAsync<T>(_ => { }, new PublishOptions());
        }

        /// <summary>
        /// Instantiates a message of type T and publishes it.
        /// </summary>
        /// <typeparam name="T">The type of message, usually an interface.</typeparam>
        /// <param name="context">The instance of <see cref="IMessageHandlerContext"/> to use for the action.</param>
        /// <param name="messageConstructor">An action which initializes properties of the message.</param>
        public static Task PublishAsync<T>(this IMessageHandlerContext context, Action<T> messageConstructor)
        {
            return context.PublishAsync(messageConstructor, new PublishOptions());
        }
    }
}