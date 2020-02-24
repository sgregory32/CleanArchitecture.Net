import { Category } from "./category";

export class Product {
  id?: number;
  categoryId: number;
  name: string;
  description?: string;
  category?: Category;
}
