import {
  TbChevronDown,
  TbChevronUp,
  TbChevronsDown,
  TbChevronsUp,
  TbEqual,
  TbEqualNot,
} from "react-icons/tb";

type PriorityIndicatorProps = {
  $priority: number;
  $size: number;
};
export default function PriorityIndicator({
  $priority,
  $size,
}: PriorityIndicatorProps) {
  switch ($priority) {
    case 0:
      return <TbEqualNot size={$size} />;
    case 1:
      return <TbChevronsDown size={$size} />;
    case 2:
      return <TbChevronDown size={$size} />;
    case 3:
      return <TbEqual size={$size} />;
    case 4:
      return <TbChevronUp size={$size} />;
    case 5:
      return <TbChevronsUp size={$size} />;
    default:
      return <TbEqualNot size={$size} />;
  }
}
