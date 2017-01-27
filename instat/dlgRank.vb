﻿' Instat-R
' Copyright (C) 2015
'
' This program is free software: you can redistribute it and/or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, or
' (at your option) any later version.
'
' This program is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License k
' along with this program.  If not, see <http://www.gnu.org/licenses/>.
Imports instat.Translations

Public Class dlgRank
    Public bFirstLoad As Boolean = True
    Private clsDefaultFunction As New RFunction
    Private Sub dlgRank_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        autoTranslate(Me)
        If bFirstLoad Then
            InitialiseDialog()
            SetDefaults()
            bFirstLoad = False
        Else
            ReopenDialog()
        End If
        'Checks if Ok can be enabled.
        TestOKEnabled()

    End Sub
    Private Sub InitialiseDialog()
        ucrBase.iHelpTopicID = 25
        ucrBase.clsRsyntax.SetFunction("rank")

        'Setting Parameters and Data types allowed
        ucrReceiverRank.Selector = ucrSelectorForRank
        ucrReceiverRank.SetMeAsReceiver()
        ucrReceiverRank.SetIncludedDataTypes({"numeric"})
        ucrReceiverRank.SetParameter(New RParameter("x"))
        ucrReceiverRank.SetParameterIsString()

        'Setting Parameters for the respective radio buttons
        ucrPanelTies.SetParameter(New RParameter("ties.method"))
        ucrPanelTies.AddRadioButton(rdoAverage, Chr(34) & "average" & Chr(34))
        ucrPanelTies.AddRadioButton(rdoMinimum, Chr(34) & "min" & Chr(34))
        ucrPanelTies.AddRadioButton(rdoMaximum, Chr(34) & "max" & Chr(34))
        ucrPanelTies.AddRadioButton(rdoFirst, Chr(34) & "first" & Chr(34))
        ucrPanelTies.AddRadioButton(rdoRandom, Chr(34) & "random" & Chr(34))
        ucrPanelTies.SetRDefault(Chr(34) & "average" & Chr(34))
        ucrPanelMissingValues.SetRDefault("FALSE")

        ucrPanelMissingValues.SetParameter(New RParameter("na.last"))
        ucrPanelMissingValues.AddRadioButton(rdoKeptAsMissing, Chr(34) & "keep" & Chr(34))
        ucrPanelMissingValues.AddRadioButton(rdoFirstMissingValues, Chr(34) & "FALSE" & Chr(34))
        ucrPanelMissingValues.AddRadioButton(rdoLast, Chr(34) & "TRUE" & Chr(34))
        ucrPanelMissingValues.SetRDefault("TRUE")


        ucrSaveRank.SetPrefix("rank")
        ucrSaveRank.SetSaveTypeAsColumn()
        ucrSaveRank.SetDataFrameSelector(ucrSelectorForRank.ucrAvailableDataFrames)
        ucrSaveRank.SetAssignToIfUncheckedValue("last_rank")
        ucrSaveRank.SetCheckBoxText("Save Rank")
        ucrSaveRank.SetIsComboBox()

    End Sub

    'Testing when to Enable the OK button
    Private Sub TestOKEnabled()
        If Not ucrReceiverRank.IsEmpty() AndAlso ucrSaveRank.IsComplete Then
            ucrBase.OKEnabled(True)
        Else
            ucrBase.OKEnabled(False)
        End If
    End Sub

    ' Sub that runs only the first time the dialog loads it sets default RFunction as the base function
    Private Sub SetDefaults()
        ucrBase.clsRsyntax.SetBaseRFunction(clsDefaultFunction.Clone())
        ucrSelectorForRank.Reset()
        SetRCode(Me, ucrBase.clsRsyntax.clsBaseFunction, True)
        ucrSelectorForRank.Reset()
        TestOKEnabled()
    End Sub

    Private Sub ReopenDialog()

    End Sub

    Private Sub Controls_ControlContentsChanged(ucrChangedControl As ucrCore) Handles ucrSelectorForRank.ControlContentsChanged, ucrReceiverRank.ControlContentsChanged, ucrSaveRank.ControlContentsChanged
        TestOKEnabled()
    End Sub

    'Private Sub SetTiesValues()

    '    If rdoAverage.Checked Then
    '        If frmMain.clsInstatOptions.bIncludeRDefaultParameters Then
    '            ucrBase.clsRsyntax.AddParameter("ties.method", Chr(34) & "average" & Chr(34))
    '        Else
    '            ucrBase.clsRsyntax.RemoveParameter("ties.method")
    '        End If
    '    ElseIf rdoMinimum.Checked Then
    '        ucrBase.clsRsyntax.AddParameter("ties.method", Chr(34) & "min" & Chr(34))
    '    ElseIf rdoMaximum.Checked Then
    '        ucrBase.clsRsyntax.AddParameter("ties.method", Chr(34) & "max" & Chr(34))
    '    ElseIf rdoFirst.Checked Then
    '        ucrBase.clsRsyntax.AddParameter("ties.method", Chr(34) & "first" & Chr(34))
    '    ElseIf rdoRandom.Checked Then
    '        ucrBase.clsRsyntax.AddParameter("ties.method", Chr(34) & "random" & Chr(34))
    '    Else
    '        ucrBase.clsRsyntax.RemoveParameter("ties.method")
    '    End If

    'End Sub

    'When the reset button is clicked, set the defaults again
    Private Sub ucrBase_ClickReset(sender As Object, e As EventArgs) Handles ucrBase.ClickReset
        SetDefaults()
    End Sub

    'Private Sub setMissingValue()
    '    If rdoKeptAsMissing.Checked Then
    '        If frmMain.clsInstatOptions.bIncludeRDefaultParameters Then
    '            ucrBase.clsRsyntax.AddParameter("na.last", Chr(34) & "keep" & Chr(34))
    '        Else
    '            ucrBase.clsRsyntax.RemoveParameter("na.last")
    '        End If
    '    ElseIf rdoFirstMissingValues.Checked Then
    '        ucrBase.clsRsyntax.AddParameter("na.last", "FALSE")
    '    ElseIf rdoLast.Checked Then
    '        ucrBase.clsRsyntax.AddParameter("na.last", "TRUE")
    '    Else
    '        ucrBase.clsRsyntax.RemoveParameter("na.last")
    '    End If
    'End Sub

End Class
