using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace ActivityManager.DBus
{
    [DBusInterface("org.kde.ActivityManager.Application")]
    interface IApplication : IDBusObject
    {
        Task quitAsync();
        Task<string> serviceVersionAsync();
        Task<bool> loadPluginAsync(string Plugin);
        Task<string[]> loadedPluginsAsync();
    }

    [DBusInterface("org.qtproject.Qt.QApplication")]
    interface IQApplication : IDBusObject
    {
        Task setStyleSheetAsync(string Sheet);
        Task setAutoSipEnabledAsync(bool Enabled);
        Task<bool> autoSipEnabledAsync();
        Task closeAllWindowsAsync();
        Task aboutQtAsync();
    }

    [DBusInterface("org.qtproject.Qt.QCoreApplication")]
    interface IQCoreApplication : IDBusObject
    {
        Task quitAsync();
    }

    [DBusInterface("org.kde.ActivityManager.Activities")]
    interface IActivities : IDBusObject
    {
        Task<string> CurrentActivityAsync();
        Task<bool> SetCurrentActivityAsync(string Activity);
        Task<string> AddActivityAsync(string Name);
        Task StartActivityAsync(string Activity);
        Task StopActivityAsync(string Activity);
        Task<int> ActivityStateAsync(string Activity);
        Task RemoveActivityAsync(string Activity);
        Task<string[]> ListActivitiesAsync();
        Task<string[]> ListActivitiesAsync(int State);
        Task<(string, string, string, double)[]> ListActivitiesWithInformationAsync();
        Task<(string, string, string, double)> ActivityInformationAsync(string Activity);
        Task<string> ActivityNameAsync(string Activity);
        Task SetActivityNameAsync(string Activity, string Name);
        Task<string> ActivityDescriptionAsync(string Activity);
        Task SetActivityDescriptionAsync(string Activity, string Description);
        Task<string> ActivityIconAsync(string Activity);
        Task SetActivityIconAsync(string Activity, string Icon);
        Task<IDisposable> WatchCurrentActivityChangedAsync(Action<string> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityAddedAsync(Action<string> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityStartedAsync(Action<string> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityStoppedAsync(Action<string> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityRemovedAsync(Action<string> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityChangedAsync(Action<string> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityNameChangedAsync(Action<(string activity, string name)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityDescriptionChangedAsync(Action<(string activity, string description)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityIconChangedAsync(Action<(string activity, string icon)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchActivityStateChangedAsync(Action<(string activity, int state)> handler, Action<Exception> onError = null);
    }

    [DBusInterface("org.kde.ActivityManager.Features")]
    interface IFeatures : IDBusObject
    {
        Task<bool> IsFeatureOperationalAsync(string Feature);
        Task<string[]> ListFeaturesAsync(string Module);
        Task<object> GetValueAsync(string Property);
        Task SetValueAsync(string Property, object Value);
    }

    [DBusInterface("org.kde.ActivityManager.Resources")]
    interface IResources : IDBusObject
    {
        Task RegisterResourceEventAsync(string Application, uint WindowId, string Uri, uint Event);
        Task RegisterResourceMimetypeAsync(string Uri, string Mimetype);
        Task RegisterResourceTitleAsync(string Uri, string Title);
    }

    [DBusInterface("org.kde.ActivityManager.ResourcesLinking")]
    interface IResourcesLinking : IDBusObject
    {
        Task LinkResourceToActivityAsync(string Agent, string Resource, string Activity);
        Task LinkResourceToActivityAsync(string Agent, string Resource);
        Task UnlinkResourceFromActivityAsync(string Agent, string Resource, string Activity);
        Task UnlinkResourceFromActivityAsync(string Agent, string Resource);
        Task<bool> IsResourceLinkedToActivityAsync(string Agent, string Resource, string Activity);
        Task<bool> IsResourceLinkedToActivityAsync(string Agent, string Resource);
        Task<IDisposable> WatchResourceLinkedToActivityAsync(Action<(string agent, string resource, string activity)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchResourceUnlinkedFromActivityAsync(Action<(string agent, string resource, string activity)> handler, Action<Exception> onError = null);
    }

    [DBusInterface("org.kde.ActivityManager.ResourcesScoring")]
    interface IResourcesScoring : IDBusObject
    {
        Task DeleteStatsForResourceAsync(string Activity, string Client, string Resource);
        Task DeleteRecentStatsAsync(string Activity, int Count, string What);
        Task DeleteEarlierStatsAsync(string Activity, int Months);
        Task<IDisposable> WatchResourceScoreUpdatedAsync(Action<(string activity, string client, string resource, double score, uint lastUpdate, uint firstUpdate)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchResourceScoreDeletedAsync(Action<(string activity, string client, string resource)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchRecentStatsDeletedAsync(Action<(string activity, int count, string what)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchEarlierStatsDeletedAsync(Action<(string activity, int months)> handler, Action<Exception> onError = null);
    }
}