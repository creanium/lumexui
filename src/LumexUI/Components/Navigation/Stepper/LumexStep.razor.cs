// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexStep
{
	/// <summary>
	/// Defines the content to be rendered inside the stepper header.
	/// </summary>
	[Parameter] public RenderFragment? StepHeader { get; set; }

	/// <summary>
	/// Defines the content to be rendered inside the stepper.
	/// </summary>
	[Parameter] public RenderFragment? StepContent { get; set; }

	[CascadingParameter] private LumexStepper Parent { get; set; } = default!;
	protected override string RootClass =>
		new CssBuilder( "lumex-step" )
			.AddClass( $"lumex-step--{_orientation.ToDescription()}" )
			.AddClass( base.RootClass )
		.Build();

	protected override string? RootStyle =>
		new StyleBuilder()
			.AddStyle( "counter-increment", "step" )
			.AddStyle( base.RootStyle )
		.NullIfEmpty();

	private Orientation _orientation;

	protected override void OnInitialized()
	{
		if( Parent is null )
		{
			throw new InvalidOperationException( $"The {nameof( LumexStep )} must be used within the {nameof( LumexStepper )} component." );
		}

		_orientation = Parent.Orientation;
	}

	protected override void OnParametersSet()
	{
		if( _orientation is Orientation.Horizontal && StepContent is not null )
		{
			throw new InvalidOperationException( $"{nameof( StepContent )} is only designed for use with the vertical stepper." );
		}
	}
}