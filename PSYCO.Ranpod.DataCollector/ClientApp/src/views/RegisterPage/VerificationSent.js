import React from 'react';
import Card from 'components/Card/Card';
import { CardContent, Typography } from '@material-ui/core';
import { CheckCircle } from '@material-ui/icons';



export default function VerificationSent(props) {


    return (
        <Card>
            <CardContent>
                <Typography align='center' color='textSecondary'>
                    <CheckCircle color='primary' />
                </Typography>


                <Typography  align='center' color='textSecondary'>
                    یک ایمیل جهت تاییدیه به شما ارسال شد.
                </Typography>

                <Typography  align='center' color='textSecondary'>
                    .لطفا جهت کار با سامانه ایمیل خود را تایید کنید
                    </Typography>
            </CardContent>
        </Card>
    )


} 