﻿using Sirenix.OdinInspector;

namespace Interfaces
{
    [HideReferenceObjectPicker]
    public interface IContract : IAddress, IAbi
    {
        public string Title { get; }
    }
}