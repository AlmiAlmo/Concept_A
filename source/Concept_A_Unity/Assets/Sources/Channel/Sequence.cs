using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using R = ChannelSystem.RadioProtocol;

namespace ChannelSystem
{
    public class Sequence
    {
        R.Step[] sequence;
        public bool isStimulReceived { get; private set; }
        public int stepNum { get; private set; }
        public R.Step step { get { return sequence[stepNum]; } }

        public void DetermineNew(R.Action stimul)
        {
            R.Step[] newSequence = null;
            if(R.orderTransmission[0].stimul == stimul) { newSequence = R.orderTransmission; }
            else if(R.orderTransmission[0].stimul == stimul) { newSequence = R.orderReception; }
            else { newSequence = null;}

            SetNewSequence(newSequence);
        }

        public Sequence(R.Step[] sequence)
        {
            SetNewSequence(sequence);
        }

        public void SetNewSequence(R.Step[] sequence)
        {
            if (sequence == null) { sequence = R.orderReception; }
            this.sequence = sequence;
            stepNum = 0;
            isStimulReceived = IsStepWithoutStimul(step);
        }

        public void SetReceptionSequence()
        {
            SetNewSequence(R.orderReception);
        }

        public void SetTransmissionSequence()
        {
            SetNewSequence(R.orderTransmission);
        }

        public R.Step NextStep()
        {
            if (IsLastStep() == false) 
            { 
                stepNum++;
                isStimulReceived = IsStepWithoutStimul(step);
            }
            return step;
        }

        public bool IsLastStep()
        {
            int sequenceMaxIndex = sequence.Length - 1;
            return (stepNum == sequenceMaxIndex);
        }

        public bool IsExpectedStimul(R.Action stimul)
        {
            if(isStimulReceived) { return false; }
            bool isRightStimul = (step.stimul == stimul);
            isStimulReceived = isRightStimul;
            return isRightStimul;
        }

        public void SetStimulIfNeeded(R.Action stimul)
        {
            bool isExpectedStimul = IsExpectedStimul(stimul);
            if(isExpectedStimul) { return; }

            DetermineNew(stimul); 

            isExpectedStimul = IsExpectedStimul(stimul);
            if (!isExpectedStimul) { Reset(); ; }
        }

        public void Reset()
        {
            SetNewSequence(null);
        }

        public bool AtStart()
        {
            return (stepNum == 0);
        }

        bool IsStepWithoutStimul(R.Step step)
        {
            return (step.stimul == R.Action.NONE);
        }
    }

}

