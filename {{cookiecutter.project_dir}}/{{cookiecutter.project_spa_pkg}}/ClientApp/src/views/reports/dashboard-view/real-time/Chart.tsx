import React, { FC } from "react";
import clsx from "clsx";
import { Bar } from "react-chartjs-2";
import { makeStyles, useTheme, Theme } from "@material-ui/core";

interface ChartProps {
  className?: string;
  data: any[];
  labels: any[];
}

const useStyles = makeStyles(() => ({
  root: {
    position: "relative",
  },
}));

const Chart: FC<ChartProps> = ({
  data: dataProp,
  labels,
  className,
  ...rest
}) => {
  const classes = useStyles();
  const theme: Theme = useTheme();

  const data = {
    datasets: [
      {
        backgroundColor: theme.palette.secondary.main,
        data: dataProp,
        barThickness: 12,
        maxBarThickness: 10,
        barPercentage: 0.9,
        categoryPercentage: 1,
      },
    ],
    labels,
  };

  const options = {
    responsive: true,
    maintainAspectRatio: false,
    animation: false,
    cornerRadius: 20,
    legend: {
      display: false,
    },
    layout: {
      padding: 0,
    },
    scales: {
      xAxes: [
        {
          stacked: false,
          gridLines: {
            display: false,
            drawBorder: false,
          },
          ticks: {
            display: false,
          },
        },
      ],
      yAxes: [
        {
          stacked: true,
          gridLines: {
            display: false,
            drawBorder: false,
          },
          ticks: {
            beginAtZero: true,
            display: false,
          },
        },
      ],
    },
    tooltips: {
      enabled: true,
      mode: "index",
      intersect: false,
      caretSize: 10,
      yPadding: 20,
      xPadding: 20,
      borderWidth: 1,
      borderColor: theme.palette.divider,
      backgroundColor: theme.palette.background.default,
      titleFontColor: theme.palette.text.primary,
      bodyFontColor: theme.palette.text.secondary,
      footerFontColor: theme.palette.text.secondary,
      callbacks: {
        legend: () => {},
        title: () => {},
        label: (tooltipItem) => {
          const label = `Views: ${tooltipItem.yLabel}`;

          return label;
        },
      },
    },
  };

  return (
    <div className={clsx(classes.root, className)} {...rest}>
      <Bar data={data} options={options} />
    </div>
  );
};
 
export default Chart;
