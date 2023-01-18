using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Simulator;
namespace PL;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>

public partial class SimulatorWindow : Window
{

    private Stopwatch stopWatch;
    private bool isTimerRun;
    BackgroundWorker timerworker;
    public SimulatorWindow()
    {
        InitializeComponent();
        stopWatch = new Stopwatch();
        timerworker = new BackgroundWorker();
        timerworker.DoWork += Worker_DoWork;
        timerworker.ProgressChanged += Worker_ProgressChanged;
        timerworker.WorkerReportsProgress = true;

    }
    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    {
        string timerText = stopWatch.Elapsed.ToString();
        timerText = timerText.Substring(0, 8);
        this.timerTextBlock.Text = timerText;
    }
    private void Worker_DoWork(object sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.RegisterForSimulationCompleteEvent(HandleSimulationComplete);
        Simulator.Simulator.RegisterForUpdateEvent(HandleSimulationUpdate);
        Simulator.Simulator.StartSimulation();
        while (isTimerRun)
        {
            timerworker.ReportProgress(1);
            Thread.Sleep(1000);
        }
    }
    private void stopTimerButton_Click(object sender, RoutedEventArgs e)
    {
        if (isTimerRun)
        {
            stopWatch.Stop();
            isTimerRun = false;
        }
    }

    private void startTimerButton_Click(object sender, RoutedEventArgs e)
    {
        if (!isTimerRun)
        {
            stopWatch.Restart();
            isTimerRun = true;
            timerworker.RunWorkerAsync();
        }
    }


    private void HandleSimulationComplete()
    {
        _simulationWorker.CancelAsync();
    }

    private void HandleSimulationUpdate(BO.Order? order,DateTime newTime)
    {

    }
}
