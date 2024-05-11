export type User = {
  id: string;
  name: string;
  avatarUrl?: string;
};

export type LoginProps = {
  email: string;
  password: string;
};

export type RegisterProps = LoginProps & {
  name: string;
};
