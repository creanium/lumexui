// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using System.Diagnostics.CodeAnalysis;
using System.Globalization;

using LumexUI.Common;
using LumexUI.Styles;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexRadioGroup<TValue> : LumexInputBase<TValue>, ISlotComponent<RadioGroupSlots>, ILumexRadioValueProvider<TValue>
{
    private readonly string _defaultGroupName = Guid.NewGuid().ToString("N");
    private RadioGroupContext<TValue>? _context;
    
    /// <summary>
    /// Gets or sets content to be rendered inside the radio group.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets the name of the group.
    /// </summary>
    [Parameter] public string? Name { get; set; }
    
    /// <summary>
    /// Gets or sets the label for the radio group.
    /// </summary>
    [Parameter] public string? Label { get; set; }

    /// <summary>
    /// Gets or sets the description for the radio group.
    /// </summary>
    [Parameter] public string? Description { get; set; }
    
    /// <summary>
    /// Gets or sets the orientation of the radio group.
    /// </summary>
    /// <remarks>
    /// The default value is <see cref="Orientation.Vertical"/>
    /// </remarks>
    [Parameter] public Orientation Orientation { get; set; } = Orientation.Vertical;
    
    /// <summary>
    /// Gets or sets the CSS class names for the radio group slots.
    /// </summary>
    [Parameter] public RadioGroupSlots? Classes { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class names for the radio button slots.
    /// </summary>
    [Parameter] public RadioSlots? RadioClasses { get; set; }

    /// <inheritdoc />
    TValue? ILumexRadioValueProvider<TValue>.CurrentValue => Value;
    
    private protected override string? RootClass =>
        TwMerge.Merge( RadioGroup.GetStyles( this ) );

    private string? LabelClass =>
        TwMerge.Merge( RadioGroup.GetLabelStyles( this ) );

    private string? WrapperClass =>
        TwMerge.Merge( RadioGroup.GetWrapperStyles( this ) );

    private string? DescriptionClass =>
        TwMerge.Merge( RadioGroup.GetDescriptionStyles( this ) );

    /// <inheritdoc />
    protected override void OnParametersSet()
    {
        // On the first render, we can instantiate the InputRadioContext
        if (_context is null)
        {
            var changeEventCallback = EventCallback.Factory.Create<ChangeEventArgs>( this, OnValueChangeAsync );
            _context = new RadioGroupContext<TValue>(this, changeEventCallback);
        }
        
        // Mutate the InputRadioContext instance in place. Since this is a non-fixed cascading parameter, the descendant
        // InputRadio/InputRadioGroup components will get notified to re-render and will see the new values.
        if (!string.IsNullOrEmpty(Name))
        {
            // Prefer the explicitly-specified group name over anything else.
            _context.GroupName = Name;
        }
        else
        {
            // Otherwise, just use a GUID to disambiguate this group's radio inputs from any others on the page.
            _context.GroupName = _defaultGroupName;
        }
    }
    
    /// <summary>
    /// Handles the change event asynchronously.
    /// Derived classes can override this to specify custom behavior when the input's value changes.
    /// </summary>
    /// <param name="args">The change event arguments.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    private async Task OnValueChangeAsync( ChangeEventArgs args )
    {
        if( Disabled || ReadOnly )
        {
            return;
        }

        await SetCurrentValueAsStringAsync( args.Value?.ToString() );
    }

    /// <inheritdoc />
    protected override ValueTask SetValidationMessageAsync( bool parsingFailed )
    {
        return ValueTask.CompletedTask;
    }
    
    /// <inheritdoc />
    protected override bool TryParseValueFromString( string? value, [MaybeNullWhen(false)] out TValue result )
    {
        try
        {
            // We special-case bool values because BindConverter reserves bool conversion for conditional attributes.
            if (typeof(TValue) == typeof(bool))
            {
                if (TryConvertToBool(value, out result))
                {
                    return true;
                }
            }
            else if (typeof(TValue) == typeof(bool?))
            {
                if (TryConvertToNullableBool(value, out result))
                {
                    return true;
                }
            }
            else if (BindConverter.TryConvertTo<TValue>(value, CultureInfo.CurrentCulture, out var parsedValue))
            {
                result = parsedValue;
                return true;
            }

            result = default;
            
            return false;
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"{Label} does not support the type '{typeof(TValue)}'.", ex);
        }
    }
    
    private static bool TryConvertToBool(string? value, out TValue result)
    {
        if (bool.TryParse(value, out var @bool))
        {
            result = (TValue)(object)@bool;
            return true;
        }

        result = default!;
        return false;
    }

    private static bool TryConvertToNullableBool(string? value, out TValue result)
    {
        if (string.IsNullOrEmpty(value))
        {
            result = default!;
            return true;
        }

        return TryConvertToBool(value, out result);
    }
}
