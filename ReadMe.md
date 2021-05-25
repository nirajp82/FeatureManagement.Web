**Microsoft.FeatureManagement** allows you to add feature flags to an ASP.NET Core app that are controlled by the configuration system.
#
**IFeatureManager**:  Used to evaluate whether a feature is enabled or disabled.
#
**IFeatureManagerSnapshot**: Cache the value for throught single request (Prevent incosist feature flag value during single request)
#
**services.AddFeatureManagement()**:  Adds required feature management services.
#
**IDisabledFeaturesHandler**: A handler that is invoked when an MVC action requires a feature and the feature is not enabled.
#
**UseMiddlewareForFeature**:  Conditionally creates a branch in the request pipeline that is rejoined to the  main pipeline.
#
**FeatureGate**:  An attribute that can be placed on MVC actions to require all or any of a set of features to be enabled. If none of the feature are enabled the registered **Microsoft.FeatureManagement.Mvc.IDisabledFeaturesHandler will be invoked.
#
**RequirementType**: All/Any: Describes whether any or all features in a given set should be required to be considered enabled.
#
#feature**:
#	negate
#	name
#
**ISessionManager**: The ISessionManager interface allows to store the state of feature flags between requests.
#
**Feature Filters**: It allows used to create dynamic feature flags (Create conditional features in application) that activate or deactivate functionality in application dynamically.
	PreSupplied feature Filters:
		- 1: Percentage Feature Filter:  It only enables a feature for x percent of requests, where x is controlled via settings. 
		- 2: Time Window Feature Filter: Enables a feature for a given time window
		- 3: Targeting Feature Filter: enables a feature flag for a specified list of users and groups, or for a specified percentage of users. TargetingFilter is "sticky." This means that once an individual user receives a feature, they'll continue to see that feature on all future requests. You can use TargetingFilter to enable a feature for a specific account during a demo, to progressively roll out new features to users in different groups or "rings," and much more.
	Creating a custom feature filter requires two things:
		- 1: Create a class that derives from IFeatureFilter.
		- 2: Optionally create a settings class to control your feature filter.```
#
References:
- https://andrewlock.net/creating-a-custom-feature-filter-adding-feature-flags-to-an-asp-net-core-app-part-4/
- https://app.pluralsight.com/library/courses/feature-flag-fundamentals-microsoft-feature-management/table-of-contents