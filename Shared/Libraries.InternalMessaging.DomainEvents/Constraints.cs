﻿using System;

namespace Libraries.InternalMessaging.DomainEvents;

public static class Constraints
{
    public static TimeSpan DefaultEventWaitTimeout { get; } = new(0, 0, 0, 10);
}