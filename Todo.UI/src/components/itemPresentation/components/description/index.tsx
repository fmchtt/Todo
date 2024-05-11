import { useRef, useState } from "react";
import { DetailsContainer } from "../../styles";
import { Editor } from "@tinymce/tinymce-react";
import { DescriptionEditContainer, DescriptionEditControls } from "./styles";
import FilledButton from "@/components/filledButton";
import { TbCheck, TbX } from "react-icons/tb";

type DescriptionProps = {
  description: string;
  onChange: (value: string) => void;
};
export default function Description(props: DescriptionProps) {
  const [isEditing, setEditing] = useState(false);
  const editorRef = useRef<Editor | null>();

  if (isEditing) {
    return (
      <DescriptionEditContainer>
        <Editor
          ref={(innerRef) => (editorRef.current = innerRef)}
          tinymceScriptSrc={`${
            import.meta.env.VITE_API_URL
          }/js/tinymce/tinymce.min.js`}
          init={{
            height: 500,
            plugins: [
              "advlist",
              "autolink",
              "lists",
              "link",
              "image",
              "charmap",
              "anchor",
              "searchreplace",
              "visualblocks",
              "code",
              "fullscreen",
              "insertdatetime",
              "media",
              "table",
              "preview",
              "help",
              "wordcount",
            ],
            language: "pt_BR",
            skin: "oxide-dark",
            content_css: "dark",
            promotion: false,
          }}
          initialValue={props.description}
        />
        <DescriptionEditControls>
          <FilledButton
            onClick={() => {
              setEditing(false);
              props.onChange(
                editorRef.current?.editor?.getBody().innerHTML || ""
              );
            }}
          >
            <TbCheck />
          </FilledButton>
          <FilledButton onClick={() => setEditing(false)}>
            <TbX />
          </FilledButton>
        </DescriptionEditControls>
      </DescriptionEditContainer>
    );
  }

  return (
    <DetailsContainer
      onClick={() => setEditing(true)}
      dangerouslySetInnerHTML={{
        __html: props.description,
      }}
    />
  );
}
