// -----------------------------------------------------------------------
// <copyright file="Primitive.cs" company="ExMod Team">
// Copyright (c) ExMod Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.API.Features.Toys
{
    using System;
    using System.Linq;

    using AdminToys;

    using Enums;
    using Exiled.API.Interfaces;
    using Exiled.API.Structs;
    using UnityEngine;

    using Object = UnityEngine.Object;

    /// <summary>
    /// A wrapper class for <see cref="PrimitiveObjectToy"/>.
    /// </summary>
    public class Primitive : AdminToy, IWrapper<PrimitiveObjectToy>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Primitive"/> class.
        /// </summary>
        /// <param name="toyAdminToyBase">The <see cref="PrimitiveObjectToy"/> of the toy.</param>
        internal Primitive(PrimitiveObjectToy toyAdminToyBase)
            : base(toyAdminToyBase, AdminToyType.PrimitiveObject) => Base = toyAdminToyBase;

        /// <summary>
        /// Gets the prefab.
        /// </summary>
        public static PrimitiveObjectToy Prefab => PrefabHelper.GetPrefab<PrimitiveObjectToy>(PrefabType.PrimitiveObjectToy);

        /// <summary>
        /// Gets the base <see cref="PrimitiveObjectToy"/>.
        /// </summary>
        public PrimitiveObjectToy Base { get; }

        /// <summary>
        /// Gets or sets the type of the primitive.
        /// </summary>
        public PrimitiveType Type
        {
            get => Base.NetworkPrimitiveType;
            set => Base.NetworkPrimitiveType = value;
        }

        /// <summary>
        /// Gets or sets the material color of the primitive.
        /// </summary>
        public Color Color
        {
            get => Base.NetworkMaterialColor;
            set => Base.NetworkMaterialColor = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the primitive can be collided with.
        /// </summary>
        public bool Collidable
        {
            get => Flags.HasFlag(PrimitiveFlags.Collidable);
            set => Flags = value ? (Flags | PrimitiveFlags.Collidable) : (Flags & ~PrimitiveFlags.Collidable);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the primitive is visible.
        /// </summary>
        public bool Visible
        {
            get => Flags.HasFlag(PrimitiveFlags.Visible);
            set => Flags = value ? (Flags | PrimitiveFlags.Visible) : (Flags & ~PrimitiveFlags.Visible);
        }

        /// <summary>
        /// Gets or sets the primitive flags.
        /// </summary>
        public PrimitiveFlags Flags
        {
            get => Base.NetworkPrimitiveFlags;
            set => Base.NetworkPrimitiveFlags = value;
        }

        /// <summary>
        /// Creates a new <see cref="Primitive"/>.
        /// </summary>
        /// <param name="position">The position of the <see cref="Primitive"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="Primitive"/>.</param>
        /// <param name="scale">The scale of the <see cref="Primitive"/>.</param>
        /// <param name="spawn">Whether the <see cref="Primitive"/> should be initially spawned.</param>
        /// <returns>The new <see cref="Primitive"/>.</returns>
        public static Primitive Create(Vector3? position = null, Vector3? rotation = null, Vector3? scale = null, bool spawn = true)
            => Create(position, rotation, scale, spawn, null);

        /// <summary>
        /// Creates a new <see cref="Primitive"/>.
        /// </summary>
        /// <param name="primitiveType">The type of primitive to spawn.</param>
        /// <param name="position">The position of the <see cref="Primitive"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="Primitive"/>.</param>
        /// <param name="scale">The scale of the <see cref="Primitive"/>.</param>
        /// <param name="spawn">Whether the <see cref="Primitive"/> should be initially spawned.</param>
        /// <returns>The new <see cref="Primitive"/>.</returns>
        public static Primitive Create(PrimitiveType primitiveType = PrimitiveType.Sphere, Vector3? position = null, Vector3? rotation = null, Vector3? scale = null, bool spawn = true)
            => Create(primitiveType, position, rotation, scale, spawn, null);

        /// <summary>
        /// Creates a new <see cref="Primitive"/>.
        /// </summary>
        /// <param name="position">The position of the <see cref="Primitive"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="Primitive"/>.</param>
        /// <param name="scale">The scale of the <see cref="Primitive"/>.</param>
        /// <param name="spawn">Whether the <see cref="Primitive"/> should be initially spawned.</param>
        /// <param name="color">The color of the <see cref="Primitive"/>.</param>
        /// <returns>The new <see cref="Primitive"/>.</returns>
        public static Primitive Create(Vector3? position /*= null*/, Vector3? rotation /*= null*/, Vector3? scale /*= null*/, bool spawn /*= true*/, Color? color /*= null*/)
        {
            Primitive primitive = new(Object.Instantiate(Prefab));

            primitive.Position = position ?? Vector3.zero;
            primitive.Rotation = Quaternion.Euler(rotation ?? Vector3.zero);
            primitive.Scale = scale ?? Vector3.one;
            primitive.Color = color ?? Color.gray;

            if (spawn)
                primitive.Spawn();

            return primitive;
        }

        /// <summary>
        /// Creates a new <see cref="Primitive"/>.
        /// </summary>
        /// <param name="primitiveType">The type of primitive to spawn.</param>
        /// <param name="position">The position of the <see cref="Primitive"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="Primitive"/>.</param>
        /// <param name="scale">The scale of the <see cref="Primitive"/>.</param>
        /// <param name="spawn">Whether the <see cref="Primitive"/> should be initially spawned.</param>
        /// <param name="color">The color of the <see cref="Primitive"/>.</param>
        /// <returns>The new <see cref="Primitive"/>.</returns>
        public static Primitive Create(PrimitiveType primitiveType /*= PrimitiveType.Sphere*/, Vector3? position /*= null*/, Vector3? rotation /*= null*/, Vector3? scale /*= null*/, bool spawn /*= true*/, Color? color /*= null*/)
        {
            Primitive primitive = new(Object.Instantiate(Prefab));

            primitive.Position = position ?? Vector3.zero;
            primitive.Rotation = Quaternion.Euler(rotation ?? Vector3.zero);
            primitive.Scale = scale ?? Vector3.one;

            primitive.Base.NetworkPrimitiveType = primitiveType;
            primitive.Color = color ?? Color.gray;

            if (spawn)
                primitive.Spawn();

            return primitive;
        }

        /// <summary>
        /// Creates a new <see cref="Primitive"/>.
        /// </summary>
        /// <param name="primitiveType">The type of primitive to spawn.</param>
        /// <param name="flags">The primitive flags.</param>
        /// <param name="position">The position of the <see cref="Primitive"/>.</param>
        /// <param name="rotation">The rotation of the <see cref="Primitive"/>.</param>
        /// <param name="scale">The scale of the <see cref="Primitive"/>.</param>
        /// <param name="spawn">Whether the <see cref="Primitive"/> should be initially spawned.</param>
        /// <param name="color">The color of the <see cref="Primitive"/>.</param>
        /// <returns>The new <see cref="Primitive"/>.</returns>
        public static Primitive Create(PrimitiveType primitiveType /*= PrimitiveType.Sphere*/, PrimitiveFlags flags, Vector3? position /*= null*/, Vector3? rotation /*= null*/, Vector3? scale /*= null*/, bool spawn /*= true*/, Color? color /*= null*/)
        {
            Primitive primitive = new(Object.Instantiate(Prefab));

            primitive.Position = position ?? Vector3.zero;
            primitive.Rotation = Quaternion.Euler(rotation ?? Vector3.zero);
            primitive.Scale = scale ?? Vector3.one;
            primitive.Flags = flags;

            primitive.Base.NetworkPrimitiveType = primitiveType;
            primitive.Color = color ?? Color.gray;

            if (spawn)
                primitive.Spawn();

            return primitive;
        }

        /// <summary>
        /// Creates a new <see cref="Primitive"/>.
        /// </summary>
        /// <param name="primitiveSettings">The settings of the <see cref="Primitive"/>.</param>
        /// <returns>The new <see cref="Primitive"/>.</returns>
        public static Primitive Create(PrimitiveSettings primitiveSettings)
        {
            Primitive primitive = new(Object.Instantiate(Prefab));

            primitive.Position = primitiveSettings.Position;
            primitive.Rotation = Quaternion.Euler(primitiveSettings.Rotation);
            primitive.Scale = primitiveSettings.Scale;
            primitive.Flags = primitiveSettings.Flags;

            primitive.Base.NetworkPrimitiveType = primitiveSettings.PrimitiveType;
            primitive.Color = primitiveSettings.Color;
            primitive.IsStatic = primitiveSettings.IsStatic;

            if (primitiveSettings.Spawn)
                primitive.Spawn();

            return primitive;
        }

        /// <summary>
        /// Gets the <see cref="Primitive"/> belonging to the <see cref="PrimitiveObjectToy"/>.
        /// </summary>
        /// <param name="primitiveObjectToy">The <see cref="PrimitiveObjectToy"/> instance.</param>
        /// <returns>The corresponding <see cref="Primitive"/> instance.</returns>
        public static Primitive Get(PrimitiveObjectToy primitiveObjectToy)
        {
            AdminToy adminToy = List.FirstOrDefault(x => x.AdminToyBase == primitiveObjectToy);
            return adminToy is not null ? adminToy as Primitive : new(primitiveObjectToy);
        }
    }
}
