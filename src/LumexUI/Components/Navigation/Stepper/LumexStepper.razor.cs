// Copyright (c) LumexUI 2024
// LumexUI licenses this file to you under the MIT license
// See the license here https://github.com/LumexUI/lumexui/blob/main/LICENSE

using LumexUI.Common;
using LumexUI.Utilities;

using Microsoft.AspNetCore.Components;

namespace LumexUI;

public partial class LumexStepper
{
	/// <summary>
	/// Defines the content to be rendered inside the stepper.
	/// </summary>
	[Parameter] public RenderFragment? ChildContent { get; set; }

	/// <summary>
	/// Defines the orientation of the stepper.
	/// </summary>
	/// <remarks>Default value: <see cref = "Orientation.Horizontal"/></remarks>
	[Parameter] public Orientation Orientation { get; set; }

	protected override string RootClass =>
		new CssBuilder( "lumex-stepper" )
			.AddClass( $"lumex-stepper--{Orientation.ToDescription()}" )
			.AddClass( base.RootClass )
		.Build();

	private List<LumexStep> _renderedSteps;
	private RenderFragment _renderSteps;
	private bool _collectingSteps;

	public LumexStepper()
	{
		_renderedSteps = new();

		_renderSteps = RenderSteps;
	}

	internal void AddStep( LumexStep step )
	{
		if( _collectingSteps )
		{
			_renderedSteps.Add( step );
		}
	}

	private void StartCollectingSteps()
	{
		_collectingSteps = true;
		_renderedSteps.Clear();
	}

	private void FinishCollectingSteps()
	{
		_collectingSteps = false;
	}
}