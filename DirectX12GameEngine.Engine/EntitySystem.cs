﻿using System;
using System.Collections.Generic;
using DirectX12GameEngine.Games;

namespace DirectX12GameEngine.Engine
{
    public abstract class EntitySystem : IGameSystem
    {
        protected EntitySystem()
        {
        }

        protected EntitySystem(Type? mainComponentType)
        {
            MainComponentType = mainComponentType;
        }

        protected EntitySystem(Type? mainComponentType, params Type[] requiredComponentTypes)
        {
            MainComponentType = mainComponentType;

            foreach (Type type in requiredComponentTypes)
            {
                RequiredComponentTypes.Add(type);
            }
        }

        public Type? MainComponentType { get; }

        public IList<Type> RequiredComponentTypes { get; } = new List<Type>();

        public EntityManager? EntityManager { get; internal set; }

        public virtual void Dispose()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void BeginDraw()
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        public virtual void EndDraw()
        {
        }

        public abstract void ProcessEntityComponent(EntityComponent component, Entity entity, bool forceRemove);

        public virtual bool Accepts(Type type) => MainComponentType?.IsAssignableFrom(type) ?? false;
    }

    public abstract class EntitySystem<TComponent> : EntitySystem where TComponent : EntityComponent
    {
        protected EntitySystem() : base(typeof(TComponent))
        {
        }

        protected EntitySystem(params Type[] requiredComponentTypes) : base(typeof(TComponent), requiredComponentTypes)
        {
        }

        protected HashSet<TComponent> Components { get; } = new HashSet<TComponent>();

        public override void ProcessEntityComponent(EntityComponent component, Entity entity, bool forceRemove)
        {
            if (!(component is TComponent entityComponent)) throw new ArgumentException("The entity component must be assignable to TComponent", nameof(component));

            bool entityMatch = !forceRemove && EntityMatch(entity);
            bool entityAdded = Components.Contains(entityComponent);

            if (entityMatch && !entityAdded)
            {
                Components.Add(entityComponent);
                OnEntityComponentAdded(entityComponent);
            }
            else if (!entityMatch && entityAdded)
            {
                Components.Remove(entityComponent);
                OnEntityComponentRemoved(entityComponent);
            }
        }

        protected virtual void OnEntityComponentAdded(TComponent component)
        {
        }

        protected virtual void OnEntityComponentRemoved(TComponent component)
        {
        }

        private bool EntityMatch(Entity entity)
        {
            if (RequiredComponentTypes.Count == 0) return true;

            List<Type> remainingRequiredTypes = new List<Type>(RequiredComponentTypes);

            foreach (EntityComponent component in entity.Components)
            {
                remainingRequiredTypes.RemoveAll(t => t.IsAssignableFrom(component.GetType()));

                if (remainingRequiredTypes.Count == 0) return true;
            }

            return false;
        }
    }
}
