using KafkaPoc.Console.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaPoc.Console.Application.Events
{
    public record ProductCreatedEvent(Product Product) : INotification;
}
